/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：SampleDatas.cs
    文件功能描述：样本数据


    创建标识：QingRain - 20211113

 ----------------------------------------------------------------*/
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;

namespace QingStack.IdentityServer.API.Services
{
    public static class SampleDatas
    {
        /// <summary>
        /// ApiResources define the apis in your system
        /// </summary>
        public static IEnumerable<IdentityResource> Ids =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource("custom_profile",new []{ JwtClaimTypes.Role,JwtRegisteredClaimNames.UniqueName})
            };

        /// <summary>
        /// Identity resources are data like user ID, name, or email address of a user
        /// </summary>
        public static IEnumerable<ApiResource> Apis => new List<ApiResource>
            {
                new ApiResource("api1", "My API",new []{ JwtClaimTypes.Role})
                {
                    Scopes= { "api1.read","api1.write" }
                },
                new ApiResource("devicecenterapi", "Device Center API",new []{ JwtClaimTypes.Role})
                {
                    Scopes= { "devicecenter"}
                }
            };

        /// <summary>
        /// client want to access resources (aka scopes)
        /// </summary>
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("api1.read",new []{ JwtRegisteredClaimNames.UniqueName}),
                new ApiScope("api1.write"),
                new ApiScope("devicecenter"),
            };

        /// <summary>
        /// client want to access resources (aka scopes)
        /// </summary>
        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "devicecenterswaggerui",
                    ClientName = "Device Center Swagger UI",
                    ClientSecrets = { new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowAccessTokensViaBrowser = true,
                    AlwaysSendClientClaims=true,
                    AlwaysIncludeUserClaimsInIdToken=true,
                    RequireConsent = true,
                    RedirectUris = { "https://localhost:6001/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { "https://localhost:6001/swagger" },
                    AllowedCorsOrigins = { "https://localhost:6001" },
                    AllowOfflineAccess=true,
                    RequirePkce = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "devicecenter",
                        "custom_profile"
                    }
                },

                // machine to machine client
                new Client
                {
                    ClientId = "client",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    
                    //ClientClaimsPrefix=string.Empty,
                    Claims=
                    {
                        new ClientClaim(JwtClaimTypes.Role, "client-role"),
                        new ClientClaim(JwtRegisteredClaimNames.UniqueName, "console-client")
                    },

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    // scopes that client has access to
                    AllowedScopes = { "api1.read" }
                },

                // interactive ASP.NET Core MVC client
                new Client
                {
                    ClientId = "mvc",
                    ClientSecrets = { new Secret("secret".Sha256())},

                    //ClientClaimsPrefix=string.Empty,
                    Claims=
                    {
                        new ClientClaim(JwtClaimTypes.Role, "client-role"),
                        new ClientClaim(JwtRegisteredClaimNames.UniqueName, "mvc-client")
                    },
                    AlwaysSendClientClaims=true,
                    AlwaysIncludeUserClaimsInIdToken=true,

                    AllowedGrantTypes = GrantTypes.Code,
                    RequireConsent = true,
                    RequirePkce = true,

                    // where to redirect to after login
                    RedirectUris = { "https://localhost:5002/signin-oidc" },

                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                         "api1.write",
                         "custom_profile"
                    },

                    AllowOfflineAccess=true,
                },

                // JavaScript Client
                new Client
                {
                    ClientId = "js",
                    ClientName = "JavaScript Client",

                    //ClientClaimsPrefix=string.Empty,
                    Claims=
                    {
                        new ClientClaim(JwtClaimTypes.Role, "client-role"),
                        new ClientClaim(JwtRegisteredClaimNames.UniqueName, "js-client")
                    },

                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,

                    RedirectUris = { "https://localhost:5003/callback.html" },
                    PostLogoutRedirectUris = { "https://localhost:5003/index.html" },
                    AllowedCorsOrigins = { "https://localhost:5003" },

                    AlwaysSendClientClaims=true,
                    AlwaysIncludeUserClaimsInIdToken=true,

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1.read",
                        "custom_profile"
                    }
                }
            };
    }
}
