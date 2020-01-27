using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models.BalimoonBML;

namespace WebApplication1.Models.MainSystem.MainSystemViewModel
{
    public class UserViewModel
    {
        public IdentityUser aspnetUser { get; set; }
        public AspNetVendor AspNetVendor { get; set; }
        public Roles roles { get; set; }
        public Vendors vendors { get; set; }
    }
}
