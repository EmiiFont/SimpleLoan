using System;
using SimpleLoans.DataAccess.Enums;

namespace SimpleLoans.LoanHelpers
{
    public class AnnuityLoan : ILoanCalculator
    {
        public override double Payment(int n)
        {

            return Principal * Rate / (1 - Math.Pow(1 + Rate, -Periods));
        }

        public override double Outstanding(int n)
        {

            return Principal * Math.Pow(1 + Rate, n) - Payment(0) * (Math.Pow(1 + Rate, n) - 1) / Rate;
        }
        public override double Interest(int n)
        {

            return Outstanding(n - 1) * Rate;

        }

        public override double Repayment(int n)
        {

            return Payment(n) + Interest(n);
        }
    }
}