using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{

    public class ForgeUser
    {
        public string userId { get; set; }
        public string userName { get; set; }
        public string emailId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string emailVerified { get; set; }
        public string _2FaEnabled { get; set; }
        public string countryCode { get; set; }
        public string language { get; set; }
        public bool optin { get; set; }
        public DateTime lastModified { get; set; }
        public Profileimages profileImages { get; set; }
        public string websiteUrl { get; set; }
    }

    public class Profileimages
    {
        public string sizeX20 { get; set; }
        public string sizeX40 { get; set; }
        public string sizeX50 { get; set; }
        public string sizeX58 { get; set; }
        public string sizeX80 { get; set; }
        public string sizeX120 { get; set; }
        public string sizeX160 { get; set; }
        public string sizeX176 { get; set; }
        public string sizeX240 { get; set; }
        public string sizeX360 { get; set; }
    }
}
