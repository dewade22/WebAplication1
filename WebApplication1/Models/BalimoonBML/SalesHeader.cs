﻿using System;
using System.Collections.Generic;

namespace WebApplication1.Models.BalimoonBML
{
    public partial class SalesHeader
    {
        public int SalesHeaderId { get; set; }
        public int DocumentType { get; set; }
        public string SalesHeaderNo { get; set; }
        public string SelltoCustomerNo { get; set; }
        public string BilltoCustomerNo { get; set; }
        public string BilltoName { get; set; }
        public string BilltoName2 { get; set; }
        public string BilltoAddress { get; set; }
        public string BilltoAddress2 { get; set; }
        public string BilltoCity { get; set; }
        public string BilltoContact { get; set; }
        public string YourReference { get; set; }
        public string ShiptoCode { get; set; }
        public string ShiptoName { get; set; }
        public string ShiptoName2 { get; set; }
        public string ShiptoAddress { get; set; }
        public string ShiptoAddress2 { get; set; }
        public string ShiptoCity { get; set; }
        public string ShiptoContact { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? PostingDate { get; set; }
        public DateTime? ShipmentDate { get; set; }
        public string PostingDescription { get; set; }
        public string PaymentTermsCode { get; set; }
        public DateTime? DueDate { get; set; }
        public decimal? PaymentDiscountPercent { get; set; }
        public DateTime? PmtDiscountDate { get; set; }
        public string ShipmentMethodCode { get; set; }
        public string LocationCode { get; set; }
        public string ShortcutDimension1Code { get; set; }
        public string ShortcutDimension2Code { get; set; }
        public string CustomerPostingGroup { get; set; }
        public string CurrencyCode { get; set; }
        public decimal? CurrencyFactor { get; set; }
        public string CustomerPriceGroup { get; set; }
        public byte? PricesIncludingVat { get; set; }
        public string InvoiceDiscCode { get; set; }
        public string CustomerDiscGroup { get; set; }
        public string LanguageCode { get; set; }
        public string SalespersonCode { get; set; }
        public string OrderClass { get; set; }
        public int? NoPrinted { get; set; }
        public string OnHold { get; set; }
        public int? AppliestoDocType { get; set; }
        public string AppliestoDocNo { get; set; }
        public string BalAccountNo { get; set; }
        public byte? Ship { get; set; }
        public byte? Invoice { get; set; }
        public string ShippingNo { get; set; }
        public string PostingNo { get; set; }
        public string LastShippingNo { get; set; }
        public string LastPostingNo { get; set; }
        public string PrepaymentNo { get; set; }
        public string LastPrepaymentNo { get; set; }
        public string PrepmtCrMemoNo { get; set; }
        public string LastPrepmtCrMemoNo { get; set; }
        public string VatregistrationNo { get; set; }
        public byte? CombineShipments { get; set; }
        public string ReasonCode { get; set; }
        public string GenBusPostingGroup { get; set; }
        public string TransactionType { get; set; }
        public string TransportMethod { get; set; }
        public string VatcountryRegionCode { get; set; }
        public string SelltoCustomerName { get; set; }
        public string SelltoCustomerName2 { get; set; }
        public string SelltoAddress { get; set; }
        public string SelltoAddress2 { get; set; }
        public string SelltoCity { get; set; }
        public string SelltoContact { get; set; }
        public string BilltoPostCode { get; set; }
        public string BilltoCountryRegionCode { get; set; }
        public string SelltoPostCode { get; set; }
        public string SelltoCountryRegionCode { get; set; }
        public string SelltoPhoneNo { get; set; }
        public string SelltoFaxNo { get; set; }
        public string ShiptoPostCode { get; set; }
        public string ShiptoCountryRegionCode { get; set; }
        public int? BalAccountType { get; set; }
        public string ExitPoint { get; set; }
        public byte? Correction { get; set; }
        public DateTime? DocumentDate { get; set; }
        public string ExternalDocumentNo { get; set; }
        public string Area { get; set; }
        public string TransactionSpecification { get; set; }
        public string PaymentMethodCode { get; set; }
        public string ShippingAgentCode { get; set; }
        public string PackageTrackingNo { get; set; }
        public string NoSeries { get; set; }
        public string PostingNoSeries { get; set; }
        public string ShippingNoSeries { get; set; }
        public string TaxAreaCode { get; set; }
        public byte? TaxLiable { get; set; }
        public string VatbusPostingGroup { get; set; }
        public int? Reserve { get; set; }
        public string AppliestoId { get; set; }
        public decimal? VatbaseDiscountPercent { get; set; }
        public int? InvoiceDiscountCalculation { get; set; }
        public decimal? InvoiceDiscountValue { get; set; }
        public byte? SendIcdocument { get; set; }
        public int? Icstatus { get; set; }
        public string SelltoIcpartnerCode { get; set; }
        public string BilltoIcpartnerCode { get; set; }
        public int? Icdirection { get; set; }
        public decimal? PrepaymentPercent { get; set; }
        public string PrepaymentNoSeries { get; set; }
        public byte? CompressPrepayment { get; set; }
        public DateTime? PrepaymentDueDate { get; set; }
        public string PrepmtCrMemoNoSeries { get; set; }
        public string PrepmtPostingDescription { get; set; }
        public DateTime? PrepmtPmtDiscountDate { get; set; }
        public string PrepmtPaymentTermsCode { get; set; }
        public decimal? PrepmtPaymentDiscountPercent { get; set; }
        public string QuoteNo { get; set; }
        public byte? AuthorizationRequired { get; set; }
        public string CreditCardNo { get; set; }
        public int? DocNoOccurrence { get; set; }
        public string CampaignNo { get; set; }
        public string SelltoCustomerTemplateCode { get; set; }
        public string SelltoContactNo { get; set; }
        public string BilltoContactNo { get; set; }
        public string BilltoCustomerTemplateCode { get; set; }
        public string OpportunityNo { get; set; }
        public string ResponsibilityCenter { get; set; }
        public int? ShippingAdvice { get; set; }
        public int? PostingfromWhseRef { get; set; }
        public DateTime? RequestedDeliveryDate { get; set; }
        public DateTime? PromisedDeliveryDate { get; set; }
        public string ShippingTime { get; set; }
        public string OutboundWhseHandlingTime { get; set; }
        public string ShippingAgentServiceCode { get; set; }
        public byte? IsReceive { get; set; }
        public string ReturnReceiptNo { get; set; }
        public string ReturnReceiptNoSeries { get; set; }
        public string LastReturnReceiptNo { get; set; }
        public byte? AllowLineDisc { get; set; }
        public byte? GetShipmentUsed { get; set; }
        public string SignatureFileName { get; set; }
        public string AssignedUserId { get; set; }
        public byte? Adjustment { get; set; }
        public byte? Basadjustment { get; set; }
        public string AdjustmentAppliesto { get; set; }
        public int? TaxDocumentType { get; set; }
        public byte? PrintedTaxDocument { get; set; }
        public byte? PostedTaxDocument { get; set; }
        public byte? TaxDocumentMarked { get; set; }
        public string CustomerQuoteNo { get; set; }
        public string CustomerOrderNo { get; set; }
        public int? SalesStatus { get; set; }
        public short RowStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedTime { get; set; }
        public decimal? AmountIncludingVat { get; set; }
    }
}