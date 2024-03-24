using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevExplorerAPI.DevExplorer.Exceptions.TaskException
{
    public class TaskException : System.Exception
    {
        public TaskException(string message) : base(message)
        {
        }
    }
}