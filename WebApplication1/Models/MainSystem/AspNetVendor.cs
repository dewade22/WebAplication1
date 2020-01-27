using System;
using System.Collections.Generic;

namespace WebApplication1.Models.MainSystem
{
    public partial class AspNetVendor
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string NpwpNo { get; set; }
        public string SiupNo { get; set; }
        public string SuplierType { get; set; }
        public string ContactName { get; set; }
        public string Contact { get; set; }
        public string ContactEmail { get; set; }
        public string InvoiceName { get; set; }
        public string InvoiceContact { get; set; }
        public string InvoiceEmail { get; set; }
        public string FileLocation { get; set; }
        public string VendorNo { get; set; }
        public string Swiftcode { get; set; }
        public string Bank_Account1 { get; set; }
        public string Bank_Account2 { get; set; }
        public string IsActive { get; set; }
        public string VendorCity { get; set; }
        public string VendorFaxNo { get; set; }
        public string Currency { get; set; }
        public string Country { get; set; }
        public string VendorPostingGroup { get; set; }
        public string PaymentTerms { get; set; }
        public string GenBusPostingGroup { get; set; }
        public string VATBusPostingGroup { get; set; }
        public string LocationCode { get; set; }

    }
}
