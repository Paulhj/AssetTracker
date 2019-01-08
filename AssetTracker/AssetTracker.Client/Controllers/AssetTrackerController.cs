using AssetTracker.Client.Services;
using AssetTracker.Client.ViewModels;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AssetTracker.Client.Controllers
{
    [Authorize]
    public class AssetTrackerController : Controller
    {
        private readonly IAssetTrackerHttpClient _assetTrackerHttpClient;

        public AssetTrackerController(IAssetTrackerHttpClient assetTrackerHttpClient)
        {
            _assetTrackerHttpClient = assetTrackerHttpClient;
        }

        public async Task<IActionResult> Index()
        {
            await WriteOutIdentityInformation();

            // call the API
            var httpClient = await _assetTrackerHttpClient.GetClient();

            var response = await httpClient.GetAsync("api/asset").ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var assetsAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                
                var assetsViewModel = new AssetsViewModel(
                    JsonConvert.DeserializeObject<IList<Model.Asset>>(assetsAsString).ToList());

                return View(assetsViewModel);
            }

            throw new Exception($"A problem happened while calling the API: {response.ReasonPhrase}");
        }

        public async Task<IActionResult> Details(int id)
        {
            // call the API
            var httpClient = await _assetTrackerHttpClient.GetClient();

            var response = await httpClient.GetAsync($"api/asset/{id}").ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var assetAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var asset = JsonConvert.DeserializeObject<Model.Asset>(assetAsString);

                return View(asset);
            }

            throw new Exception($"A problem happened while calling the API: {response.ReasonPhrase}");
        }

        [Authorize(Roles = "PowerUser")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost()]
        [Authorize(Roles = "PowerUser")]
        public async Task<IActionResult> Create(Model.AssetForCreation model)
        {
            // call the API
            var httpClient = await _assetTrackerHttpClient.GetClient();

            var response = await httpClient.PostAsync(
                $"api/asset",
                new StringContent(JsonConvert.SerializeObject(model), System.Text.Encoding.Unicode, "application/json"))
                .ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            throw new Exception($"A problem happened while calling the API: {response.ReasonPhrase}");
        }

        [Authorize(Policy = "CanOrderAsset")]
        public async Task<IActionResult> OrderAsset()
        {
            var discoveryClient = new DiscoveryClient("https://localhost:44374/");
            var metaDataResponse = await discoveryClient.GetAsync();
            var userInfoClient = new UserInfoClient(metaDataResponse.UserInfoEndpoint);

            var accessToken = await HttpContext
                .GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

            var response = await userInfoClient.GetAsync(accessToken);

            if (response.IsError)
            {
                throw new Exception(
                    "Problem accessing the UserInfo endpoint."
                    , response.Exception);
            }

            var address = response.Claims.FirstOrDefault(c => c.Type == "address")?.Value;

            return View(new OrderFromViewModel(address));
        }

        public async Task WriteOutIdentityInformation()
        {
            // get the saved identity token
            var identityToken = await HttpContext
                .GetTokenAsync(OpenIdConnectParameterNames.IdToken);

            // write it out
            Debug.WriteLine($"Identity token: {identityToken}");

            // write out the user claims
            foreach (var claim in User.Claims)
            {
                Debug.WriteLine($"Claim type: {claim.Type} - Claim value: {claim.Value}");
            }
        }

        public async Task Logout()
        {
            // get the metadata
            var discoveryClient = new DiscoveryClient("https://localhost:44374/");
            var metaDataResponse = await discoveryClient.GetAsync();

            // create a TokenRevocationClient
            var revocationClient = new TokenRevocationClient(
                metaDataResponse.RevocationEndpoint,
                "assettrackerclient",
                "secret");

            // get the access token to revoke 
            var accessToken = await HttpContext
              .GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                var revokeAccessTokenResponse =
                    await revocationClient.RevokeAccessTokenAsync(accessToken);

                if (revokeAccessTokenResponse.IsError)
                {
                    throw new Exception("Problem encountered while revoking the access token."
                        , revokeAccessTokenResponse.Exception);
                }
            }

            // revoke the refresh token as well
            var refreshToken = await HttpContext
             .GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);

            if (!string.IsNullOrWhiteSpace(refreshToken))
            {
                var revokeRefreshTokenResponse =
                    await revocationClient.RevokeRefreshTokenAsync(refreshToken);

                if (revokeRefreshTokenResponse.IsError)
                {
                    throw new Exception("Problem encountered while revoking the refresh token."
                        , revokeRefreshTokenResponse.Exception);
                }
            }

            await HttpContext.SignOutAsync("Cookies");
            await HttpContext.SignOutAsync("oidc");
        }
    }
}
