using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models.BalimoonBML;
using WebApplication1.Models.ViewModel;

namespace WebApplication1.Models.ViewModel
{
    public class PurchaseRequestViewModel
    {
        public IEnumerable<PurchaseRequisitionHeader> PRHeaderList { get; set; }
        public PurchaseRequisitionHeader PRHeaderDetail { get; set; }
        public IEnumerable<PurchaseRequisitionLine> PRLineList { get; set; }
        public PurchaseRequisitionLine PRLineDetail { get; set; }
        public Items itemDetail { get; set; }
        public IEnumerable<Items> itemList { get; set; }


        public IEnumerable<ItemPRViewModel> ItemsPRList { get; set; }
    }
}
