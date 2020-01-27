using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models.BalimoonBML;
using WebApplication1.Models.MainSystem;

namespace WebApplication1.Models.ViewModel
{
    public class VendorViewModel
    {
        //BML Context
        public Vendors vendors { get; set; }
        public IEnumerable<Vendors> vendorList { get; set; }
        //Main System
        public AspNetVendor aspVendors { get; set; }
        public IEnumerable<AspNetVendor> aspVendorsList { get; set; }
    }
}
