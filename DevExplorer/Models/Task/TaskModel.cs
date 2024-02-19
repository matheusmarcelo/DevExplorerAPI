using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExplorerAPI.DevExplorer.Models.Step;
using DevExplorerAPI.DevExplorer.Models.User;

namespace DevExplorerAPI.DevExplorer.Models.Task
{
    public class TaskModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public UserModel User { get; set; }
        public List<StepModel> Steps { get; set; }
    }
}