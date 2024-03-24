using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExplorerAPI.DevExplorer.Interfaces.IAuth;
using DevExplorerAPI.DevExplorer.Models.DapperContext;
using DevExplorerAPI.DevExplorer.Models.User;
using DevExplorerAPI.DevExplorer.Services.AuthService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevExplorerAPI.DevExplorer.Controllers.AuthController
{
    [ApiController]
    [Route("v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticateService _authenticateService;

        public AuthController(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }

        [AllowAnonymous]
        [HttpPost, Route("login")]
        public async Task<IActionResult> LoginAsync([FromBody] AuthenticateModel authModel)
        {
            try
            {
                var user = await _authenticateService.LoginAsync(authModel);
                if (!user)
                    return Unauthorized();

                UserModel userExists = await _authenticateService.GetUserByEmailAsync(authModel.Email);
                var token = _authenticateService.GenerateToken(userExists.Id, userExists.Email);

                var userDetails = new
                {
                    id = userExists.Id,
                    name = userExists.Name,
                    cpf = userExists.Cpf,
                    email = userExists.Email,
                    birthDay = userExists.BirthDay,
                    celPhone = userExists.CelPhone,
                    address = userExists.Address,
                    house_number = userExists.House_number,
                    state = userExists.State,
                    zip_code = userExists.Zip_code,
                    naturality = userExists.Nat,
                    token = token
                };

                return Ok(userDetails);
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}