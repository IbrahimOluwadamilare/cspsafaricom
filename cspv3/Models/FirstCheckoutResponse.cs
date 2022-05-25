using System;
namespace cspv3.Models
{
    public partial class FirstCheckoutResponse
    {
        public bool Status { get; set; }
        public string StatusCode { get; set; }
        public string Message { get; set; }
        public Data Data { get; set; }
        public string Validation { get; set; }
    }

    public partial class Data
    {
        public string Status { get; set; }
        public string StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public string Amount { get; set; }
        public string AmountPaid { get; set; }
        public string PaymentTransactionReference { get; set; }
        public string TransactionReference { get; set; }
        public string Currency { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Narration { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DatePaid { get; set; }
        public string DateReversed { get; set; }
    }
}
