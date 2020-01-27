using System;
using System.Collections.Generic;

namespace WebApplication1.Models.BalimoonBML
{
    public partial class GenLedgerEntryTemp
    {
        public string AccountNo { get; set; }
        public DateTime? Date { get; set; }
        public string DocNo { get; set; }
        public string Desc { get; set; }
        public decimal? Db { get; set; }
        public decimal? Cr { get; set; }
    }
}
