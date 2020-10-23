using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using System.Web;

namespace ForgeBlazored.Services
{
    public class ConfigurationManager
    {
        public string ClientId => "your client id";
        public string ClientSecret => "your client secret";

        //change it with the one defined when the app is created
        public string CallbackUrl => "http://localhost:3000/api/forge/callback/oauth"; 
        public string Host { get; set; } = "developer.api.autodesk.com";
        public Uri AuthorizeUrlCode => new Uri($"https://{Host}/authentication/v1/authorize?response_type=code&client_id={ClientId}&redirect_uri={UrlSafeCallbackUrl}&scope=data:read%20data:write");
        public string UrlSafeCallbackUrl => HttpUtility.UrlEncode(this.CallbackUrl);

    }
}
