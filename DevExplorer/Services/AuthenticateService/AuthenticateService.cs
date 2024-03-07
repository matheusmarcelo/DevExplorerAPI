using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExplorerAPI.DevExplorer.Helper;
using DevExplorerAPI.DevExplorer.Interfaces.IAuth;
using DevExplorerAPI.DevExplorer.Models.DapperContext;
using DevExplorerAPI.DevExplorer.Models.User;

namespace DevExplorerAPI.DevExplorer.Services.AuthService
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly IAuthenticateRepository _authenticate;

        public AuthenticateService(IAuthenticateRepository authenticate)
        {
            this._authenticate = authenticate;
        }

        public string GenerateToken(int id, string email)
        {
            try
            {
                var token = _authenticate.GenerateToken(id, email);
                return token;
            }
            catch (System.Exception)
            {

                throw new Exception();
            }
        }

        public async Task<UserModel> GetUserByEmailAsync(string email)
        {
            try
            {
                var user = await _authenticate.GetUserByEmailAsync(email);
                return user;
            }
            catch (System.Exception)
            {

                throw new Exception();
            }
        }

        public async Task<bool> LoginAsync(AuthenticateModel authenticate)
        {
            try
            {
                authenticate.Password = authenticate.Password.GeneratePasswordHash();
                var user = await _authenticate.LoginAsync(authenticate);

                if (user)
                {
                    return true;
                }

                return false;
            }
            catch (System.Exception)
            {

                throw new Exception();
            }
        }

    }
}