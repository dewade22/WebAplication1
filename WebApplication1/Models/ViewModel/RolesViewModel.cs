using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models.MainSystem;

namespace WebApplication1.Models.ViewModel
{
    public class RolesViewModel
    {
        public Roles RoleDetail { get; set; }
        public IEnumerable<Roles> RoleList { get; set; }
    }
}
