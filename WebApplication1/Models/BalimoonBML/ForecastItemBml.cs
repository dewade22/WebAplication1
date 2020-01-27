using System;
using System.Collections.Generic;

namespace WebApplication1.Models.BalimoonBML
{
    public partial class ForecastItemBml
    {
        public int ItemBudgetEntryId { get; set; }
        public int EntryNo { get; set; }
        public int? AnalysisArea { get; set; }
        public string BudgetName { get; set; }
        public DateTime? Date { get; set; }
        public string ItemNo { get; set; }
        public int? SourceType { get; set; }
        public string SourceNo { get; set; }
        public string Description { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Liters { get; set; }
        public decimal? CostAmount { get; set; }
        public decimal? SalesAmount { get; set; }
        public string BudgetDimension1Code { get; set; }
        public string ItemDescription { get; set; }
        public int? Quarter { get; set; }
        public int? Tahun { get; set; }
        public int? Bulan { get; set; }
        public string Packaging { get; set; }
        public int? LinkId { get; set; }
    }
}
