﻿using System;
using System.Collections.Generic;

namespace WebApplication1.Models.BalimoonBML
{
    public partial class GenJournalBatch
    {
        public int GenJournalBatchId { get; set; }
        public string JournalTemplateName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ReasonCode { get; set; }
        public int? BalAccountType { get; set; }
        public string BalAccountNo { get; set; }
        public string NoSeries { get; set; }
        public string PostingNoSeries { get; set; }
        public byte? CopyVatsetuptoJnlLines { get; set; }
        public byte? AllowVatdifference { get; set; }
        public short RowStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedTime { get; set; }
    }
}
