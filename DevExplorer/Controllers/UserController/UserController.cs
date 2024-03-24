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

        [AllowAnonymous]
        [HttpGet, Route("{CPF}")]
        public async Task<IActionResult> GetUserAsync(string CPF)
        {
            try
            {
                UserModel user = await _userService.GetUserAsync(CPF);
                return Ok(user);
            }
            catch (System.Exception)
            {

                throw new Exception();
            }
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
                    return BadRequest("Não foi possivel cadastrar o usuario.");
                }

            }
            catch (System.Exception)
            {

                throw new Exception();
            }
        }

        [Authorize]
        [HttpPut, Route("update-user")]
        public async Task<IActionResult> UpdateUserAsync([FromBody] UserModel user)
        {
            try
            {
                bool userUpdated = await _userService.UpdateUserAsync(user);

                if (userUpdated)
                {
                    return Ok("Usuario atualizado com sucesso!");
                }
                else
                {
                    throw new Exception("É necessario preencher os campos obrigatórios.");
                }

            }
            catch (System.Exception)
            {

                throw new Exception();
            }
        }

        [Authorize]
        [HttpPut, Route("delete-user/{CPF}")]
        public async Task<IActionResult> DeleteUserAsync(string CPF)
        {
            try
            {
                UserModel user = await _userService.DeleteUserAsync(CPF);

                if (user != null)
                {
                    return Ok($"Usuario {user.Name} deletado com sucesso!");
                }
                else
                {
                    throw new Exception("É necessario preencher os campos obrigatórios.");
                }

            }
            catch (System.Exception)
            {

                throw new Exception();
            }
        }
    }
}