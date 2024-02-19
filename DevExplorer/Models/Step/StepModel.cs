using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevExplorerAPI.DevExplorer.Models.Step
{
    public class StepModel
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
    }
}