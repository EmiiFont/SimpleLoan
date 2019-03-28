using System;
using System.Collections.Generic;
using SimpleLoans.DataAccess.Enums;
using SimpleLoans.DataAccess.Models;

namespace SimpleLoans.LoanHelpers
{
    public class SimplePersonalLoan : ILoanCalculator
    {   
        public int TermPayment { get; set; }
        
        public SimplePersonalLoan()
        {
            
        }
        public SimplePersonalLoan(double principal, double rate, int periods, int termPayment)
        {
            this.Principal = principal;
            this.Rate = rate;
            this.Periods = periods;
            this.TermPayment = termPayment;
        }
        public override double Payment(int n)
        {
            return Repayment(n) + Interest(n);
        }

        public override double Outstanding(int n)
        {
            return Principal - (Repayment(0) * n);
        }
        public override double Interest(int n)
        {
            return Principal * Rate;
        }

        public override double Repayment(int n)
        {
            if (n >= Periods && Outstanding(n) > 0)
            {
                return Principal - (NewRepayment * Periods);
            }

            if (Math.Abs(Remainder) > 0)
            {
                return NewRepayment;
            }
            return  Principal / Periods;
        }

        public double ExtraPayment(int n)
        {
            if (n >= Periods && Outstanding(n) > 0)
            {
                return Principal - (NewRepayment * Periods);
            }
            return Principal / Periods;
        }

       
    }
}