using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models.BalimoonBML;

namespace WebApplication1.Models.MainSystem.MainSystemViewModel
{
    public class AspVendorViewModel
    {
        public IEnumerable<AspNetVendor> VendorList { get; set; }
        public AspNetVendor VendorDetail { get; set; }
        public IdentityUser Users { get; set; }
        public Vendors BMLVendorList { get; set; }
    }
}
