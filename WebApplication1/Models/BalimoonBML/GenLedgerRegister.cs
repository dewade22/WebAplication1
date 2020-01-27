﻿using System;
using System.Collections.Generic;

namespace WebApplication1.Models.BalimoonBML
{
    public partial class GenLedgerRegister
    {
        public int GenLedgerRegisterId { get; set; }
        public int No { get; set; }
        public int? FromEntryNo { get; set; }
        public int? ToEntryNo { get; set; }
        public DateTime? CreationDate { get; set; }
        public string SourceCode { get; set; }
        public string UserId { get; set; }
        public string JournalBatchName { get; set; }
        public int? FromVatentryNo { get; set; }
        public int? ToVatentryNo { get; set; }
        public byte? Reversed { get; set; }
        public short RowStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedTime { get; set; }
    }
}
