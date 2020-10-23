using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ForgeBlazored.Services
{
    public class TokenManager : ITokenManager
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ConfigurationManager _configurationManager;
        public ForgeUser _user { get; set; }
        public Token _token { get; set; }

        public bool Configured { get; set; } = false;
        public TokenManager(IHttpClientFactory clientFactory, ConfigurationManager configurationManager)
        {
            _clientFactory = clientFactory;
            _configurationManager = configurationManager;
        }

        public async Task<Token> GetAccessToken(bool forceRefresh = false)
        {
            if (_token == null)
            {
                throw new InvalidOperationException("Error, null token, have you called configure?");
            }

            if (DateTime.UtcNow >= _token.ExpiresOn || forceRefresh)
            {
                await RefreshToken();
            }
            return _token;
        }

        public async Task<ForgeUser> GetForgeUser(string token)
        {
            if (token != null)
            {
                using (var client = new HttpClient())
                using (var req = new HttpRequestMessage(HttpMethod.Get,$"https://{_configurationManager.Host}/userprofile/v1/users/@me")){
                    req.Headers.Add("Authorization", $"Bearer {_token.AccessToken}");
                    using (var resp = await client.SendAsync(req))
                    {
                        if (resp.IsSuccessStatusCode)
                        {
                            var jsonUser = await resp.Content.ReadAsStringAsync();
                            _user = JsonConvert.DeserializeObject<ForgeUser>(jsonUser);
                        }
                    }
                }
                return _user;
            }
            return new ForgeUser();
        }

        public async Task RefreshToken()
        {
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage(HttpMethod.Post, $"https://{_configurationManager.Host}/authentication/v1/refreshtoken"))
            {
                var body = $"client_id={_configurationManager.ClientId}&client_secret={_configurationManager.ClientSecret}&grant_type=refresh_token&refresh_token={_token.Refresh}";

                request.Content = new StringContent(body, Encoding.UTF8, "application/x-www-form-urlencoded");

                using (var resp = await client.SendAsync(request))
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        var json = await resp.Content.ReadAsStringAsync();

                        _token = JsonConvert.DeserializeObject<Token>(json);

                        _token.ExpiresOn = DateTime.UtcNow.AddSeconds(_token.ExpiresIn) - TimeSpan.FromMinutes(5); ;
                    }
                    else
                    {
                        throw new InvalidOperationException("Error, token refresh failed.");
                    }
                }
            }
        }

        public async Task Configure(string code)
        {
            _token = null;
            using (var client = _clientFactory.CreateClient()){
                using (var request = new HttpRequestMessage(
                    HttpMethod.Post, 
                    $"https://{_configurationManager.Host}/authentication/v1/gettoken"
                    )
                    ){
                    var body = $"client_id={_configurationManager.ClientId}&client_secret={_configurationManager.ClientSecret}&grant_type=authorization_code&code={code}&redirect_uri={_configurationManager.CallbackUrl}";
                    request.Content = new StringContent(body, Encoding.UTF8, "application/x-www-form-urlencoded");
                    using (var response = await client.SendAsync(request))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var json = await response.Content.ReadAsStringAsync();
                            _token = JsonConvert.DeserializeObject<Token>(json);
                            _token.ExpiresOn = DateTime.UtcNow.AddSeconds(_token.ExpiresIn) - TimeSpan.FromMinutes(10);

                            Configured = true;
                        }
                    }
                }

                
            }
            

        }

    }
}
