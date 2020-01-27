using System;
using System.Collections.Generic;

namespace WebApplication1.Models.BalimoonBML
{
    public partial class Detail
    {
        public string JournalTemplateName { get; set; }
        public string JournalBatchName { get; set; }
        public string SourceCode { get; set; }
        public string SeqLineNo { get; set; }
        public string AccountNo { get; set; }
        public string JournalDescription { get; set; }
        public DateTime? DocumentDate { get; set; }
        public DateTime? PostingDate { get; set; }
        public string DocumentNo { get; set; }
        public string Description { get; set; }
        public double? DebitAmount { get; set; }
        public double? CreditAmount { get; set; }
        public double? Amount { get; set; }
        public DateTime? CreatedTime { get; set; }
    }
}
