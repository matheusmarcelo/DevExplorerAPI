using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExplorerAPI.DevExplorer.Interfaces.IUserService;
using DevExplorerAPI.DevExplorer.Models.User;
using DevExplorerAPI.DevExplorer.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevExplorerAPI.DevExplorer.Controllers.UserController
{
    [ApiController]
    [Route("v1/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [AllowAnonymous]
        [HttpPost, Route("create")]
        public async Task<IActionResult> AddUserAsync([FromBody] UserModel user)
        {
            try
            {
                bool userCreated = await _userService.AddUserAsync(user);

                if (userCreated)
                {
                    return Ok("Usuario cadastrado com sucesso!");
                }
                else
                {
                    throw new Exception("Parece que houve um erro ao criar o usuario :(");
                }

            }
            catch (System.Exception)
            {

                throw new Exception();
            }
        }

        [AllowAnonymous]
        [HttpGet, Route("user-list")]
        public async Task<IActionResult> GetUsersAsync()
        {
            try
            {
                IEnumerable<UserModel> users = await _userService.GetUsersAsync();
                return Ok(users);
            }
            catch (System.Exception)
            {

                throw new Exception();
            }
        }
    }
}