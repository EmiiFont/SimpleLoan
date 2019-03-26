using System;

namespace SimpleLoans.Models
{
    public class Loan
    {
        public decimal Amount { get; set; }
        public double Rate { get; set; }
        public int Terms { get; set; }
        public DateTime CreationDate { get; set; }
        public PaymentType PaymentType { get; set; }
    }
}