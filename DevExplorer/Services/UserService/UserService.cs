using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExplorerAPI.DevExplorer.Helper;
using DevExplorerAPI.DevExplorer.Interfaces.IUser;
using DevExplorerAPI.DevExplorer.Interfaces.IUserService;
using DevExplorerAPI.DevExplorer.Models.User;
using DevExplorerAPI.DevExplorer.Repositories.UserRepository;

namespace DevExplorerAPI.DevExplorer.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserModel> GetUserAsync(string CPF)
        {
            try
            {
                UserModel user = await _userRepository.GetUserAsync(CPF);
                return user;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public async Task<IEnumerable<UserModel>> GetUsersAsync()
        {
            IEnumerable<UserModel> users = await _userRepository.GetUsersAsync();

            if (users.Any())
            {
                return users;
            }

            throw new Exception("Não existe usuarios para listar");
        }

        public async Task<bool> AddUserAsync(UserModel user)
        {
            user.Password = user.Password.GeneratePasswordHash();
            bool userCreated = await _userRepository.AddUserAsync(user);

            if (userCreated)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public async Task<bool> UpdateUserAsync(UserModel user)
        {
            try
            {
                bool userUpdated = await _userRepository.UpdateUserAsync(user);

                return userUpdated;

            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public async Task<UserModel> DeleteUserAsync(string CPF)
        {
            try
            {
                UserModel user = await _userRepository.DeleteUserAsync(CPF);
                return user;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
}