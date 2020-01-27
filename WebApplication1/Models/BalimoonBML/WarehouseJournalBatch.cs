﻿using System;
using System.Collections.Generic;

namespace WebApplication1.Models.BalimoonBML
{
    public partial class WarehouseJournalBatch
    {
        public int WarehouseJournalBatchId { get; set; }
        public string JournalTemplateName { get; set; }
        public string Name { get; set; }
        public string LocationCode { get; set; }
        public string Description { get; set; }
        public string ReasonCode { get; set; }
        public string NoSeries { get; set; }
        public string RegisteringNoSeries { get; set; }
        public string AssignedUserId { get; set; }
        public short RowStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedTime { get; set; }
    }
}
