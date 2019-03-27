using System;
using SimpleLoans.DataAccess.Models;

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
        public double Principal { get; set; }
        public double Rate { get; set ; }
        public int Periods { get; set; }
        public PaymentType PaymentType { get; set; }

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

        public double Interest(int n)
        {
             return Outstanding(n - 1) * Rate;
        }

        public double Outstanding(int n)
        {
           return Repayment(0) * (Periods - n);
        }

        public double Payment(int n)
        {
             return Repayment(n) + Interest(n);
        }

        public double Repayment(int n)
        {
            return Principal / Periods;
        }
    }
}