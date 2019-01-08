using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace Jsd.IDP
{
    public static class Config
    {
        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "9",
                    Username = "Roger",
                    Password = "password",

                    Claims = new List<Claim>
                    {
                        new Claim("given_name", "Roger"),
                        new Claim("family_name", "Williams"),
                        new Claim("address", "Main Road 1"),
                        new Claim("role", "PowerUser"),
                        new Claim("subscriptionlevel", "FreeUser"),
                        new Claim("country", "usa")
                    }
                },
                new TestUser
                {
                    SubjectId = "10",
                    Username = "John",
                    Password = "password",

                    Claims = new List<Claim>
                    {
                        new Claim("given_name", "John"),
                        new Claim("family_name", "Routt"),
                        new Claim("address", "Big Street 2"),
                        new Claim("role", "PowerUser"),
                        new Claim("subscriptionlevel", "PayingUser"),
                        new Claim("country", "usa")
                    }
                },
                new TestUser
                {
                    SubjectId = "11",
                    Username = "Davey",
                    Password = "password",

                    Claims = new List<Claim>
                    {
                        new Claim("given_name", "Davey"),
                        new Claim("family_name", "Crockett"),
                        new Claim("address", "Big Street 2"),
                        new Claim("role", "BasicUser"),
                        new Claim("subscriptionlevel", "PayingUser"),
                        new Claim("country", "usa")
                    }
                }
            };
        }

        // identity-related resources (scopes)
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Address(),
                new IdentityResource(
                    "roles",
                    "Your role(s)",
                     new List<string>() { "role" }),
                new IdentityResource(
                    "country",
                    "The country you're living in",
                    new List<string>() { "country" }),
                new IdentityResource(
                    "subscriptionlevel",
                    "Your subscription level",
                    new List<string>() { "subscriptionlevel" })
            };
        }

        // api-related resources (scopes)
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("assettrackerapi", "Asset Tracker Api",
                new List<string>() { "role" })
                {
                    ApiSecrets = { new Secret("apisecret".Sha256()) }
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>()
            {
                new Client
                {
                    ClientName = "Asset Tracker",
                    ClientId = "assettrackerclient",
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    AccessTokenType = AccessTokenType.Reference,
                    //IdentityTokenLifetime = ...
                    //AuthorizationCodeLifetime = ...
                    AccessTokenLifetime = 120,
                    AllowOfflineAccess = true,
                    //AbsoluteRefreshTokenLifetime = ...
                    UpdateAccessTokenClaimsOnRefresh = true,
                    RedirectUris = new List<string>()
                    {
                        "https://localhost:44332/signin-oidc"
                    },
                    PostLogoutRedirectUris = new List<string>()
                    {
                        "https://localhost:44332/signout-callback-oidc"
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Address,
                        "roles",
                        "assettrackerapi",
                        "country",
                        "subscriptionlevel"
                    },
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    }
                }
             };
        }
    }
}
