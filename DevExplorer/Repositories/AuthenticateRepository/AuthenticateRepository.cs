using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DevExplorerAPI.DevExplorer.Interfaces.IAuth;
using DevExplorerAPI.DevExplorer.Models.DapperContext;
using DevExplorerAPI.DevExplorer.Models.User;
using Microsoft.IdentityModel.Tokens;

namespace DevExplorerAPI.DevExplorer.Repositories.AuthRepository
{
    public class AuthenticateRepository : IAuthenticateRepository
    {
        private readonly DapperContext _dapperContext;
        private readonly IDbConnection _connection;
        private readonly IConfiguration _configuration;

        public AuthenticateRepository(DapperContext dapperContext, IConfiguration configuration)
        {
            _dapperContext = dapperContext;
            _connection = _dapperContext.CreateConnection();
            _configuration = configuration;
        }

        public string GenerateToken(int id, string email)
        {
            var claims = new[]
            {
                new Claim("id", id.ToString()),
                new Claim("email", email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:secretKey"]));
            var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddDays(1);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["JWT:issuer"],
                audience: _configuration["JWT:audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<UserModel> GetUserByEmailAsync(string email)
        {
            try
            {
                string query = @"SELECT * FROM Users WHERE Email = @Email";
                var user = await _connection.QueryFirstOrDefaultAsync<UserModel>(query, new { Email = email });

                if (user == null)
                {
                    throw new Exception();
                }

                return user;
            }
            catch (System.Exception)
            {

                throw new Exception("Não foi possivel realizar login, email ou senha incorretos.");
            }
        }

        public async Task<bool> LoginAsync(AuthenticateModel authenticate)
        {
            string query = @"SELECT * FROM Users WHERE Email = @Email and Password = @Password";
            var user = await _connection.QueryFirstOrDefaultAsync(query, new { Email = authenticate.Email, Password = authenticate.Password });

            if (user == null)
            {
                return false;
            }

            return true;
        }


    }
}