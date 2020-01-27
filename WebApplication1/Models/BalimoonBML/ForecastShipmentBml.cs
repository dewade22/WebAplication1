using System;
using System.Collections.Generic;

namespace WebApplication1.Models.BalimoonBML
{
    public partial class ForecastShipmentBml
    {
        public int SalesShipmentLineId { get; set; }
        public int SalesShipmentHeaderId { get; set; }
        public string DocumentNo { get; set; }
        public int SeqLineNo { get; set; }
        public string SelltoCustomerNo { get; set; }
        public int? RecordType { get; set; }
        public string RecordNo { get; set; }
        public string LocationCode { get; set; }
        public string PostingGroup { get; set; }
        public DateTime? ShipmentDate { get; set; }
        public string Description { get; set; }
        public string Description2 { get; set; }
        public string UnitofMeasure { get; set; }
        public decimal? QuantityOrder { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? UnitCostLcy { get; set; }
        public decimal? Vatpercent { get; set; }
        public decimal? LineDiscountPercent { get; set; }
        public decimal? LineDiscountAmount { get; set; }
        public decimal? Amount { get; set; }
        public decimal? AmountIncludingVat { get; set; }
        public decimal? QuantityInvoiced { get; set; }
        public string OrderNo { get; set; }
        public int? OrderLineNo { get; set; }
        public string BilltoCustomerNo { get; set; }
        public string PurchaseOrderNo { get; set; }
        public string GenBusPostingGroup { get; set; }
        public string GenProdPostingGroup { get; set; }
        public decimal? VatbaseAmount { get; set; }
        public decimal? UnitCost { get; set; }
        public DateTime? PostingDate { get; set; }
        public decimal? QtyperUnitofMeasure { get; set; }
        public string UnitofMeasureCode { get; set; }
        public decimal? QuantityBase { get; set; }
        public decimal? QtyInvoicedBase { get; set; }
        public int? Tahun { get; set; }
        public int? Quarter { get; set; }
        public int? Bulan { get; set; }
        public int? LinkId { get; set; }
        public decimal? LiterQty { get; set; }
        public decimal? QtyLiter { get; set; }
    }
}
