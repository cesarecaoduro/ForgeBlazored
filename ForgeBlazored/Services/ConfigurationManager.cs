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
        public string ClientId => "oRaHB1hVUDNhQAXutnM0RGMJr5Kma1me";
        public string ClientSecret => "YcNhkCqhAtsxbPXi";
        public string CallbackUrl => "http://localhost:3000/api/forge/callback/oauth";
        public string AccountId => "fd3ba5bb-521d-4350-9a79-356fa769f2d0";
        public string ProjectId => "f9ae7992-f818-460b-92a7-1954e07a86d5";
        public string BIM360ProjectId => $"b.{ProjectId}";
        public string BIM360HubId => $"b.{AccountId}";
        public string Host { get; set; } = "developer.api.autodesk.com";
        public Uri AuthorizeUrlCode => new Uri($"https://{Host}/authentication/v1/authorize?response_type=code&client_id={ClientId}&redirect_uri={UrlSafeCallbackUrl}&scope=data:read%20data:write");
        public string UrlSafeCallbackUrl => HttpUtility.UrlEncode(this.CallbackUrl);

    }
}
