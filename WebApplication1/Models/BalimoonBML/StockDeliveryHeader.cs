﻿using System;
using System.Collections.Generic;

namespace WebApplication1.Models.BalimoonBML
{
    public partial class StockDeliveryHeader
    {
        public int StockDeliveryHeaderId { get; set; }
        public string DocumentNo { get; set; }
        public DateTime? DocumentDate { get; set; }
        public DateTime? PostingDate { get; set; }
        public string FromLocationCode { get; set; }
        public string ToLocationCode { get; set; }
        public string JournalDescription { get; set; }
        public string ProdOrderNo { get; set; }
        public string StockReqNo { get; set; }
        public int? StockReqType { get; set; }
        public int? DocStatus { get; set; }
        public short RowStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedTime { get; set; }
        public string RequestFor { get; set; }
    }
}
