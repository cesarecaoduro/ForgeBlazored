using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Token
    {
        [JsonProperty("access_token")] 
        public string AccessToken { get; set; }
        [JsonProperty("expires_in")] 
        public double ExpiresIn { get; set; }
        
        [JsonProperty("expires_on")]
        public DateTime ExpiresOn { get; set; }

        [JsonProperty("refresh_token")] 
        public string Refresh { get; set; }
    }
}
