using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForgeBlazored.Services
{
    public interface ITokenManager
    {
        public bool Configured { get; set; }
        public Task<Token> GetAccessToken(bool forceRefresh = false);
        public Task Configure(string code);
        public Task<ForgeUser> GetForgeUser(string token);
    }
}
