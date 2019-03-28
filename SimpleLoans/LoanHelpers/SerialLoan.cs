using System;
using SimpleLoans.DataAccess.Enums;

namespace SimpleLoans.LoanHelpers
{
    public class SerialLoan : ILoanCalculator
    {
        public SerialLoan()
        {
            
        }
        public SerialLoan(double principal, double rate, int periods, int typeOfTerms)
        {
            Principal = principal;
            Rate = rate;
            Periods = periods;
        }
        public override double Interest(int n)
        {
             return Outstanding(n - 1) * Rate;
        }

        public override double Outstanding(int n)
        {
           return Repayment(0) * (Periods - n);
        }

        public override double Payment(int n)
        {
             return Repayment(n) + Interest(n);
        }

        public override double Repayment(int n)
        {
            return Principal / Periods;
        }
    }
}