using IdentityServer4.Models;

namespace Intern.IdentityServer
{
    public class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource
                {
                    Name = "role",
                    UserClaims = new List<string> { "role" }
                }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new[] { new ApiScope("InternAPI.read"), new ApiScope("InternAPI.write"), };
        public static IEnumerable<ApiResource> ApiResources =>
            new[]
            {
                new ApiResource("InternAPI")
                {
                    Scopes = new List<string> { "InternAPI.read", "InternAPI.write" },
                    ApiSecrets = new List<Secret> { new Secret("ScopeSecret".Sha256()) },
                    UserClaims = new List<string> { "role" }
                }
            };

        public static IEnumerable<Client> Clients =>
            new[]
            {
                // m2m client credentials flow client
                new Client
                {
                    ClientId = "m2m.client",
                    ClientName = "Client Credentials Client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("ClientSecret1".Sha256()) },
                    AllowedScopes = { "InternAPI.read", "InternAPI.write" }
                },
                // interactive client using code flow + pkce
                new Client
                {
                    ClientId = "interactive",
                    ClientSecrets = { new Secret("ClientSecret1".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = { "https://localhost:5444/signin-oidc" },
                    FrontChannelLogoutUri = "https://localhost:5444/signout-oidc",
                    PostLogoutRedirectUris = { "https://localhost:5444/signout-callback-oidc" },
                    AllowOfflineAccess = true,
                    AllowedScopes = { "openid", "profile", "InternAPI.read" },
                    RequirePkce = true,
                    RequireConsent = true,
                    AllowPlainTextPkce = false
                },
            };
    }
}