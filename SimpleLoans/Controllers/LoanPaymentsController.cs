using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SimpleLoans.DataAccess.Models;
using SimpleLoans.LoanHelpers;

namespace SimpleLoans.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanPaymentsController : ControllerBase
    {
        private ILoanCalculator loanCalculator;
        public LoanPaymentsController()
        {
            
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<LoanPayments>> Get()
        {
            return new LoanPayments[] { new LoanPayments()};
        }
        
        [HttpPost]
        public void Post([FromBody] LoanPayments borrower)
        {
            loanCalculator = new SerialLoan();
            loanCalculator.Principal = Convert.ToDouble(borrower.Loan.Amount);
            loanCalculator.Rate = (borrower.Loan.Rate / 100);
            loanCalculator.Periods = borrower.Loan.Terms;
            loanCalculator.PaymentType = borrower.Loan.PaymentType;

        }
    }
}