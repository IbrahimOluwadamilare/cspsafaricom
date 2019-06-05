using System;
using System.Collections.Generic;

namespace CSBGlobal.Models
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public string CspOrderId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyAddress { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public decimal Total { get; set; }
        public bool Payment { get; set; }

        public string PaymentTransactionReference { get; set; }

        public string PaymentGateWay { get; set; }
        public string BillingCycle { get; set; }

        public DateTime PaymentDate { get; set; }

        public string Domain { get; set; }
        public bool FulfillPayment { get; set; }
        public System.DateTime OrderDate { get; set; }
        public string Refinfo { get; set; }
        public string PromoCode { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public string Path { get; set; }
        public string AuthorizationPayment { get; set; }
        public System.DateTime FulFillmentDate { get; set; }
        public System.DateTime LastPaymentDate { get; set; }
        public System.DateTime NextPaymentDate { get; set; }



    }
}
