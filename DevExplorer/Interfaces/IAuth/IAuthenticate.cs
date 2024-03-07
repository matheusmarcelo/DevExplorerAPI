using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExplorerAPI.DevExplorer.Models.DapperContext;
using DevExplorerAPI.DevExplorer.Models.User;

namespace DevExplorerAPI.DevExplorer.Interfaces.IAuth
{
    public interface IAuthenticateService
    {
        public Task<bool> LoginAsync(AuthenticateModel authenticate);
        public string GenerateToken(int id, string email);
        public Task<UserModel> GetUserByEmailAsync(string email);
    }
}