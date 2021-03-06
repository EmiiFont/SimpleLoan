using System.Collections.Generic;

namespace SimpleLoans.DataAccess.Models
{
    public class LoanPayments
    {
        public Borrower Borrower { get; set; }
        public Loan Loan { get; set; }
        public IEnumerable<Payments> Payments { get; set; }
    }
}