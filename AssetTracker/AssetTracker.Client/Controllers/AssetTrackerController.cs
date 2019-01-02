using AssetTracker.Client.Services;
using AssetTracker.Client.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetTracker.Client.Controllers
{
    public class AssetTrackerController : Controller
    {
        private readonly IAssetTrackerHttpClient _assetTrackerHttpClient;

        public AssetTrackerController(IAssetTrackerHttpClient assetTrackerHttpClient)
        {
            _assetTrackerHttpClient = assetTrackerHttpClient;
        }

        public async Task<IActionResult> Index()
        {
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
    }
}
