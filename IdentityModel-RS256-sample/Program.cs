using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using RestSharp;

namespace ConsoleApp
{
    class Program
    {
        class TokenResponse
        {
            public string access_token { get; set; }
            public string scope { get; set; }
            public int expires_in { get; set; }
            public string token_type { get; set; }
        }

        //const string auth0Domain = "https://jerrie.auth0.com/"; // Your Auth0 domain
        const string auth0Domain = "https://dev12.eu.auth0.com/";
        const string auth0Audience = "https://dev12.eu.auth0.com/api/v2/";//"https://rs256.test.api"; // Your API Identifier

        static string GetToken()
        {
            /*
            var client = new RestClient("https://dev1234.auth0.com/oauth/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\"client_id\":\"S14UHRO2bRWT48ud7jsECim3bhiTbp1c\",\"client_secret\":\"fspU12kvKReIczMCCf6XDbPArB_8lcUcsvTS-oyyE6_nihVK5mLGQ6ElVh6Ocf0g\",\"audience\":\"https://dev1234.auth0.com/api/v2/\",\"grant_type\":\"client_credentials\"}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            */
            var client = new RestClient($"{auth0Domain}oauth/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\"client_id\":\"E8BhOg0q9hE1J1IvctE09tgES0DfGSB9\",\"client_secret\":\"MxVrNGE7nwR-P6Ir0ujkh2_sbC9P_TAY3YuLQk6rEysBuGBOVY8S3ra3VC6AYvyY\",\"audience\":\"" + auth0Audience + "\",\"grant_type\":\"client_credentials\"}", ParameterType.RequestBody);
            IRestResponse responseApi = client.Execute(request);
            var response = JsonConvert.DeserializeObject<TokenResponse>(responseApi.Content);
            return response.access_token;
        }


        static void Main(string[] args)
        {
            var token = GetToken();
            Validate_JWT_Token(token);
        }

        static void Validate_JWT_Token(string testTokenParam)
        {
            string testToken = testTokenParam;// ""; // Obtain a JWT to validate and put it in here

            try
            {
                // Download the OIDC configuration which contains the JWKS
                // NB!!: Downloading this takes time, so do not do it very time you need to validate a token, Try and do it only once in the lifetime
                //     of your application!!
                IConfigurationManager<OpenIdConnectConfiguration> configurationManager = new ConfigurationManager<OpenIdConnectConfiguration>($"{auth0Domain}.well-known/openid-configuration", new OpenIdConnectConfigurationRetriever());
                OpenIdConnectConfiguration openIdConfig = AsyncHelper.RunSync(async () => await configurationManager.GetConfigurationAsync(CancellationToken.None));

                // Configure the TokenValidationParameters. Assign the SigningKeys which were downloaded from Auth0. 
                // Also set the Issuer and Audience(s) to validate
                TokenValidationParameters validationParameters =
                    new TokenValidationParameters
                    {
                        ValidIssuer = auth0Domain,
                        ValidAudiences = new[] { auth0Audience },
                        IssuerSigningKeys = openIdConfig.SigningKeys
                    };

                // Now validate the token. If the token is not valid for any reason, an exception will be thrown by the method
                SecurityToken validatedToken;
                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                var user = handler.ValidateToken(testToken, validationParameters, out validatedToken);

                // The ValidateToken method above will return a ClaimsPrincipal. Get the user ID from the NameIdentifier claim
                // (The sub claim from the JWT will be translated to the NameIdentifier claim)
                Console.WriteLine($"Token is validated. User Id {user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error occurred while validating token: {e.Message}");
                throw;
            }

            Console.WriteLine();
            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();
        }
    }
}