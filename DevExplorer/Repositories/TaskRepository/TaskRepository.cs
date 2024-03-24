using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DevExplorerAPI.DevExplorer.Exceptions.TaskException;
using DevExplorerAPI.DevExplorer.Interfaces.ITask;
using DevExplorerAPI.DevExplorer.Models.DapperContext;
using DevExplorerAPI.DevExplorer.Models.Task;

namespace DevExplorerAPI.DevExplorer.Repositories.TaskRepository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly DapperContext _dapperContext;
        private readonly IDbConnection _connection;

        public TaskRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
            _connection = _dapperContext.CreateConnection();
        }

        public async Task<List<TaskModel>> GetTasks()
        {
            string query = @"SELECT * FROM Tasks";
            List<TaskModel> tasks = (await _connection.QueryAsync<TaskModel>(query, new { })).ToList();

            return tasks;
        }

        public async Task<TaskModel> AddTaskAsync(TaskModel model)
        {
            _connection.Open();
            IDbTransaction transaction = _connection.BeginTransaction();
            try
            {
                string query = @"INSERT INTO Tasks (UserId, Title, Description, Status)
                VALUES (@UserId, @Title, @Description, @Status); SELECT SCOPE_IDENTITY();";

                var taskId = (await _connection.QueryAsync<int>(query, model, transaction)).Single();


                query = @"SELECT Id, Title, Description FROM Tasks WHERE Id = @Id";

                TaskModel task = await _connection.QuerySingleOrDefaultAsync<TaskModel>(query, new { Id = taskId }, transaction);

                transaction.Commit();
                return task;
            }
            catch (Exception)
            {

                try
                {
                    transaction.Rollback();
                }
                catch (TaskException)
                {
                    throw new TaskException("Não foi possivel cadastrar a tarefa!");
                }
                throw;
            }
            finally
            {
                _connection.Close();
            }
        }
    }
}