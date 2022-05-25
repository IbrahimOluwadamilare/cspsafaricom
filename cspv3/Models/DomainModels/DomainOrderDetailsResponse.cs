using System;
using System.Collections.Generic;

namespace cspv3.Models.DomainModels
{
    public partial class DomainOrderDetailsResponse
    {
        public long OrderId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string Currency { get; set; }
        public List<Item> Items { get; set; }
        public Pricing Pricing { get; set; }
        public List<Payment> Payments { get; set; }
        public BillTo BillTo { get; set; }
    }

    public partial class BillTo
    {
        public Contact Contact { get; set; }
    }

    public partial class Contact
    {
        public AddressMailing AddressMailing { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string Phone { get; set; }
        public string NameFirst { get; set; }
        public string NameMiddle { get; set; }
        public string NameLast { get; set; }
        public string Organization { get; set; }
    }

    public partial class AddressMailing
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public long PostalCode { get; set; }
        public string State { get; set; }
    }

    public partial class Item
    {
        public string Label { get; set; }
        public long Quantity { get; set; }
        public long Pfid { get; set; }
        public string PeriodUnit { get; set; }
        public long Period { get; set; }
        public TaxCollector TaxCollector { get; set; }
        public List<string> Domains { get; set; }
        public Pricing Pricing { get; set; }
    }

    public partial class Pricing
    {
        public long Subtotal { get; set; }
        public long List { get; set; }
        public long Savings { get; set; }
        public long? Sale { get; set; }
        public long Discount { get; set; }
        public long Taxes { get; set; }
        public Fees Fees { get; set; }
        public Pricing Unit { get; set; }
        public List<object> TaxDetails { get; set; }
        public long? Total { get; set; }
    }

    public partial class Fees
    {
        public long Total { get; set; }
        public long Icann { get; set; }
    }

    public partial class TaxCollector
    {
        public long TaxCollectorId { get; set; }
    }

    public partial class Payment
    {
        public long Amount { get; set; }
        public long PaymentProfileId { get; set; }
        public string Category { get; set; }
    }

}
