﻿using System;
using System.Collections.Generic;

namespace WebApplication1.Models.BalimoonBML
{
    public partial class VendorLedgerEntryDetailed
    {
        public int VendorLedgerEntryDetailedId { get; set; }
        public int EntryNo { get; set; }
        public int? VendorLedgerEntryNo { get; set; }
        public int? EntryType { get; set; }
        public DateTime? PostingDate { get; set; }
        public int? DocumentType { get; set; }
        public string DocumentNo { get; set; }
        public decimal? Amount { get; set; }
        public decimal? AmountLcy { get; set; }
        public string VendorNo { get; set; }
        public string CurrencyCode { get; set; }
        public string UserId { get; set; }
        public string SourceCode { get; set; }
        public int? TransactionNo { get; set; }
        public string JournalBatchName { get; set; }
        public string ReasonCode { get; set; }
        public decimal? DebitAmount { get; set; }
        public decimal? CreditAmount { get; set; }
        public decimal? DebitAmountLcy { get; set; }
        public decimal? CreditAmountLcy { get; set; }
        public DateTime? InitialEntryDueDate { get; set; }
        public string InitialEntryGlobalDim1 { get; set; }
        public string InitialEntryGlobalDim2 { get; set; }
        public string GenBusPostingGroup { get; set; }
        public string GenProdPostingGroup { get; set; }
        public byte? UseTax { get; set; }
        public string VatbusPostingGroup { get; set; }
        public string VatprodPostingGroup { get; set; }
        public int? InitialDocumentType { get; set; }
        public int? AppliedVendLedgerEntryNo { get; set; }
        public byte? Unapplied { get; set; }
        public int? UnappliedbyEntryNo { get; set; }
        public decimal? RemainingPmtDiscPossible { get; set; }
        public decimal? MaxPaymentTolerance { get; set; }
        public string TaxJurisdictionCode { get; set; }
        public short RowStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedTime { get; set; }
    }
}
