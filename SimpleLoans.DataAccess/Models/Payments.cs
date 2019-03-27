using System;

namespace SimpleLoans.DataAccess.Models
{
    public class Payments
    {
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public bool Paid { get; set; }
        public string Comments { get; set; }
    }
}