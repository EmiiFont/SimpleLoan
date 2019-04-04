using System;
using System.Collections.Generic;
using SimpleLoans.DataAccess.Enums;
using SimpleLoans.DataAccess.Models;

namespace SimpleLoans.LoanHelpers
{
    public abstract class ILoanCalculator
    {
        //define principal property
        public double Principal { get; set; }
        //define rate property
        public double Rate { get; set; }
        //definte periods property
        public int Periods { get; set; }
        public PaymentType PaymentType { get; set; }
        public double Remainder { get; set; }
        public double NewRepayment { get; set; }

        public virtual double Payment(int n)
        {
          return n;
        }

        public virtual double Outstanding(int n)
        {

           return n;
        }

        public virtual double Interest(int n)
        {
            return n;
        }

        public virtual double Repayment(int n)
        {
            return n;
        }

         public virtual void ChangePayment(double newPayment, bool remainderToCapital = false)
        {
            if (remainderToCapital)
            {
                Remainder = newPayment - ((Principal / Periods) + (Principal * Rate));
                NewRepayment = (Principal / Periods) + Remainder;
                Periods = Convert.ToInt32(Principal / NewRepayment);
            }
            else
            {
                var remainder = newPayment - Payment(0);
                Rate = ((Interest(0) + remainder) / Principal);    
            }
        }

        public virtual DateTime GetNextPaymentDate(DateTime startDate)
        {
              switch (PaymentType)
            {
                case PaymentType.Daily:
                    return startDate.AddDays(1);
                //weekly
                case PaymentType.Weekly:
                    return startDate.AddDays(7);
                //bi-weekly
                case PaymentType.BiWeekly:
                    return startDate.AddDays(14);
                //monthly
                case PaymentType.Monthly:
                    return startDate.AddMonths(1);
            }

            return startDate;
        }

        public virtual IEnumerable<Payments> GetAmortizationTable()
        {
            var tab = new List<Payments>();
            
            var date = DateTime.Now;

            for (int n = 1; n <= Periods; ++n)
            {
                date = GetNextPaymentDate(date);

                tab.Add(new Payments
                {
                    Amount = (decimal)Payment(n),
                    Repayment = Repayment(n),
                    Interest = Interest(n),
                    Outstanding = Outstanding(n),
                    PaymentDate = date,
                });
            }

            return tab;
        }
    }
}