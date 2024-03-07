using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExplorerAPI.DevExplorer.Models.User;

namespace DevExplorerAPI.DevExplorer.Interfaces.IUser
{
    public interface IUserRepository
    {
        Task<bool> UserExistsAsync(string email);
        Task<IEnumerable<UserModel>> GetUsersAsync();
        Task<UserModel> GetUserAsync(string CPF);
        Task<bool> AddUserAsync(UserModel user);
        Task<bool> UpdateUserAsync(UserModel user);
        Task<UserModel> DeleteUserAsync(string CPF);
    }
}