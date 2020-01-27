using System;
using System.Collections.Generic;

namespace WebApplication1.Models.BalimoonBML
{
    public partial class VwGenJournalDetail
    {
        public int GenJournalHeaderId { get; set; }
        public int GenJournalLineId { get; set; }
        public string JournalTemplateName { get; set; }
        public string JournalBatchName { get; set; }
        public string AccountNo { get; set; }
        public DateTime? PostingDate { get; set; }
        public int? AccountType { get; set; }
        public int? DocumentType { get; set; }
        public string DocumentNo { get; set; }
        public string Description { get; set; }
        public string BalAccountNo { get; set; }
        public decimal? DebitAmount { get; set; }
        public decimal? CreditAmount { get; set; }
        public decimal? Amount { get; set; }
        public decimal? AmountLcy { get; set; }
        public string VatprodPostingGroup { get; set; }
    }
}
