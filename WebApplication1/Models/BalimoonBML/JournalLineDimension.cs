﻿using System;
using System.Collections.Generic;

namespace WebApplication1.Models.BalimoonBML
{
    public partial class JournalLineDimension
    {
        public int JournalLineDimensionId { get; set; }
        public string TableName { get; set; }
        public string JournalTemplateName { get; set; }
        public string JournalBatchName { get; set; }
        public int JournalLineNo { get; set; }
        public int AllocationLineNo { get; set; }
        public string DimensionCode { get; set; }
        public string DimensionValueCode { get; set; }
        public string NewDimensionValueCode { get; set; }
        public short RowStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedTime { get; set; }
    }
}
