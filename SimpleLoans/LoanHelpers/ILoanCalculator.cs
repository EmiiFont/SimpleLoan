using System;
using SimpleLoans.DataAccess.Enums;

namespace SimpleLoans.LoanHelpers
{
    public interface ILoanCalculator
    {
        //define principal property
        double Principal { get; set; }
        //define rate property
        double Rate { get; set; }
        //definte periods property
        int Periods { get; set; }

        PaymentType PaymentType { get; set; }

        double Payment(int n);

        double Outstanding(int n);

        double Interest(int n);

        double Repayment(int n);

        DateTime GetNextPaymentDate(DateTime startDate); 
    }
}