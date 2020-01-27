using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models.MainSystem;

namespace WebApplication1.Models.ViewModel
{
    public class UsersRoleViewModel
    {
        public Roles rolesDetail { get; set; }
        public IEnumerable<Roles> rolesList { get; set; }
        public IdentityUser userDetail { get; set; }
        public IEnumerable<IdentityUser> userList { get; set; }
    }
}
