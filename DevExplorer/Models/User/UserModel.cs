using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExplorerAPI.DevExplorer.Models.Task;

namespace DevExplorerAPI.DevExplorer.Models.User
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string? Password { get; set; }
        public DateTime? BirthDay { get; set; }
        public string? CelPhone { get; set; }
        public string? DDD { get; set; }
        public string? DDI { get; set; }
        public string? Address { get; set; }
        public string? House_number { get; set; }
        public string? Zip_code { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? Nat { get; set; }
        public string? Status_account { get; set; }
        public DateTime Date_account { get; set; } = DateTime.Now;
        public DateTime Date_updated_account { get; set; } = DateTime.Now;
        public List<TaskModel>? Tasks { get; set; }
    }
}