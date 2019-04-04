using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SimpleLoans.DataAccess.BogusRepository;
using SimpleLoans.DataAccess.Models;
using SimpleLoans.LoanHelpers;

namespace SimpleLoans.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanPaymentsController : ControllerBase
    {
        private ILoanCalculator loanCalculator;
        private readonly IBorrowerRepository _borrowerRepo;
        public LoanPaymentsController(IBorrowerRepository borrowerRepo)
        {
            _borrowerRepo = borrowerRepo;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<LoanPayments>> Get()
        {
            return new LoanPayments[] { new LoanPayments()};
        }

        [HttpPost("GetPayments")]
        public ActionResult<IEnumerable<Payments>> GetPayments(Loan loan)
        {
            var dbd = _borrowerRepo;
            dbd.Add();
            
            loanCalculator = new AnnuityLoan();
            loanCalculator.Principal = Convert.ToDouble(loan.Amount);
            loanCalculator.Rate = (loan.Rate / 100);
            loanCalculator.Periods = loan.Terms;
            loanCalculator.PaymentType = loan.PaymentType;
            
            var dlist = new List<Payments>();
            var date = DateTime.Now;
            
            for (int n = 1; n <= loanCalculator.Periods; ++n)
            {
                date = loanCalculator.GetNextPaymentDate(date);
                
                var payment = new Payments();
                payment.Amount = (decimal)loanCalculator.Payment(n);
                payment.PaymentDate = date;
                payment.Interest = loanCalculator.Interest(n);
                payment.Outstanding = loanCalculator.Outstanding(n);
                payment.Repayment = loanCalculator.Repayment(n);
                 
                dlist.Add(payment);
            }
            return dlist;
        }
        
        [HttpPost]
        public void Post([FromBody] LoanPayments borrower)
        {
            
        }
    }
}