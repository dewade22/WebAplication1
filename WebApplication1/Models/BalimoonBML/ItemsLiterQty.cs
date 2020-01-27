using System;
using System.Collections.Generic;

namespace WebApplication1.Models.BalimoonBML
{
    public partial class ItemsLiterQty
    {
        public int ItemId { get; set; }
        public string ItemNo { get; set; }
        public string Description { get; set; }
        public string InventoryPostingGroup { get; set; }
        public decimal? LiterQty { get; set; }
    }
}
