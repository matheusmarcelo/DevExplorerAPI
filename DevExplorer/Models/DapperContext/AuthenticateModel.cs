using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevExplorerAPI.DevExplorer.Models.DapperContext
{
    public class AuthenticateModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}