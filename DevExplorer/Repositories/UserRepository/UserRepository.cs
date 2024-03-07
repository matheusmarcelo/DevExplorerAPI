using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DevExplorerAPI.DevExplorer.Helper;
using DevExplorerAPI.DevExplorer.Interfaces.IUser;
using DevExplorerAPI.DevExplorer.Models.DapperContext;
using DevExplorerAPI.DevExplorer.Models.Step;
using DevExplorerAPI.DevExplorer.Models.Task;
using DevExplorerAPI.DevExplorer.Models.User;

namespace DevExplorerAPI.DevExplorer.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperContext _dapperContext;
        private readonly IDbConnection connection;

        public UserRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
            connection = _dapperContext.CreateConnection();
        }

        public async Task<bool> UserExistsAsync(string email)
        {
            string query = @"SELECT * FROM Users Where Email = @Email";
            var userExists = await connection.QueryFirstOrDefaultAsync(query, new { Email = email });

            if (userExists == null)
            {
                return false;
            }

            return true;
        }

        public async Task<IEnumerable<UserModel>> GetUsersAsync()
        {
            /* Descomentar quando já tiver Tasks e Steps incluidos no banco */
            // string query = @"select * from users t1 left join Tasks t2 on t2.UserId = t1.Id
            // left join Steps t3 on t3.TaskId = t2.Id where t1.Id = t1.Id";

            // var user = await connection.QueryAsync<UserModel, TaskModel, StepModel, UserModel>(
            //     query,
            //     (user, task, step) =>
            //     {
            //         task.Steps = new List<StepModel>();
            //         user.Tasks = new List<TaskModel>();
            //         task.Steps.Add(step);
            //         user.Tasks.Add(task);
            //         return user;
            //     }
            //     );
            // return user;

            return await connection.QueryAsync<UserModel>("SELECT * FROM Users");
        }

        public async Task<UserModel> GetUserAsync(string CPF)
        {
            string query = @"SELECT * FROM Users WHERE Cpf = @CPF";
            UserModel user = await connection.QuerySingleOrDefaultAsync<UserModel>(query, new { CPF = CPF });

            if (user != null)
                return user;

            throw new Exception("Não existe usuario com este CPF");
        }

        public async Task<bool> AddUserAsync(UserModel user)
        {
            IDbConnection conn = connection;
            var userExists = UserExistsAsync(user.Email).Result;

            if (userExists)
            {
                return false;
            }


            string query = @"
            INSERT INTO Users (Name, Age, Cpf, Email, BirthDay, Password, CelPhone, DDD, DDI, Address, House_number, Zip_code, State, City, Nat, Status_account, Date_account, Date_updated_account)
            VALUES (@Name, @Age, @Cpf, @Email, @BirthDay, @Password, @CelPhone, @DDD, @DDI, @Address, @House_number, @Zip_code, @State, @City, @Nat, @Status_account, @Date_account, @Date_updated_account)";

            var returnQuery = await conn.QueryAsync<UserModel>(
                query,
                new
                {
                    Name = user.Name,
                    Age = user.Age,
                    Cpf = user.Cpf,
                    Email = user.Email,
                    BirthDay = user.BirthDay,
                    Password = user.Password,
                    CelPhone = user.CelPhone,
                    DDD = user.DDD,
                    DDI = user.DDI,
                    Address = user.Address,
                    House_number = user.House_number,
                    @Zip_code = user.Zip_code,
                    State = user.State,
                    City = user.City,
                    Nat = user.Nat,
                    Status_account = 'A',
                }
            );

            if (returnQuery != null)
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
            string query = @"UPDATE Users SET 
                                Name = @Name,
                                Age = @Age,
                                Cpf = @Cpf,
                                Email = @Email,
                                BirthDay = @BirthDay,
                                CelPhone = @CelPhone,
                                DDD = @DDD,
                                DDI = @DDI,
                                Address = @Address,
                                House_number = @House_number,
                                Zip_code = @Zip_code,
                                State = @State,
                                City = @City,
                                Nat = @Nat,
                                Date_updated_account = @Date_updated_account
                                WHERE Cpf = @Cpf";
            if (user != null)
            {
                var userUpdated = await connection.QueryAsync<UserModel>(query,
                new
                {
                    Name = user.Name,
                    Age = user.Age,
                    Cpf = user.Cpf,
                    Email = user.Email,
                    BirthDay = user.BirthDay,
                    Password = user.Password,
                    CelPhone = user.CelPhone,
                    DDD = user.DDD,
                    DDI = user.DDI,
                    Address = user.Address,
                    House_number = user.House_number,
                    @Zip_code = user.Zip_code,
                    State = user.State,
                    City = user.City,
                    Nat = user.Nat,
                    Date_updated_account = DateTime.Now
                }
                );

                if (userUpdated != null)
                {
                    return true;
                }


            }

            return false;
        }

        public async Task<UserModel> DeleteUserAsync(string CPF)
        {
            string query = @"SELECT Name FROM Users WHERE Cpf = @CPF";

            string queryDelete = @"UPDATE Users SET Status_account = 'E' WHERE Cpf = @CPF";

            await connection.ExecuteAsync(queryDelete, new { CPF = CPF });

            UserModel user = await connection.QuerySingleAsync<UserModel>(query, new { CPF = CPF });

            if (user != null)
            {
                return user;
            }


            return null;
        }
    }
}