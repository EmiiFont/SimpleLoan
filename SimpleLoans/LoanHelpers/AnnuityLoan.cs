using System;
using SimpleLoans.DataAccess.Models;

namespace SimpleLoans.LoanHelpers
{
    public class AnnuityLoan : ILoanCalculator
    {
        public double Principal { get; set; }
        public double Rate { get; set; }
        public int Periods { get; set; }
        public PaymentType PaymentType { get; set; }

        public double Payment(int n)
        {

            return Principal * Rate / (1 - Math.Pow(1 + Rate, -Periods));
        }

        public double Outstanding(int n)
        {

            return Principal * Math.Pow(1 + Rate, n) - Payment(0) * (Math.Pow(1 + Rate, n) - 1) / Rate;
        }
        public double Interest(int n)
        {

            return Outstanding(n - 1) * Rate;

        }

        public double Repayment(int n)
        {

            return Payment(n) + Interest(n);
        }

        public DateTime GetNextPaymentDate(DateTime startDate)
        {
               switch (PaymentType)
            {
                case PaymentType.Daily:
                    return startDate.AddDays(1);
                    break;
                //weekly
                case PaymentType.Weekly:
                    return startDate.AddDays(7);
                    break;
                //bi-weekly
                case PaymentType.BiWeekly:
                    return startDate.AddDays(14);
                    break;
                //monthly
                case PaymentType.Monthly:
                    return startDate.AddMonths(1);
                    break;
            }

            return startDate;
        }
    }
}