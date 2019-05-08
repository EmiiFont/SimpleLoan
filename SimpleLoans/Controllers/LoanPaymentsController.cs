using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IBorrowerRepository _borrowerRepo;
        private SimplePersonalLoan loanCalculator;
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
        public ActionResult<LoanPayments> GetPayments(Loan loan)
        {
            loanCalculator = new SimplePersonalLoan();
            loanCalculator.Principal = Convert.ToDouble(loan.Amount);
            loanCalculator.Rate = (loan.Rate / 100);
            loanCalculator.Periods = loan.Terms;
            loanCalculator.PaymentType = loan.PaymentType;
            
            if(loan.PaymentSetByUser != 0)
            {
                loanCalculator.ChangePayment(Convert.ToDouble(loan.PaymentSetByUser), loan.ReminderToCapital);
            }
            
            var loanPayments = new LoanPayments();
            loanPayments.Loan = new Loan()
            {
                Amount = (decimal)loanCalculator.Principal,
                Rate = (loanCalculator.Rate * 100),
                Terms = loanCalculator.Periods,
                PaymentType = loanCalculator.PaymentType,
                PaymentSetByUser = loan.PaymentSetByUser,
                ReminderToCapital = loan.ReminderToCapital,
                CreationDate = DateTime.UtcNow
            };
            loanPayments.Payments = loanCalculator.GetAmortizationTable().ToList();
            
            return loanPayments;
        }
        
        [HttpPost]
        public void Post([FromBody] LoanPayments loanPayments)
        {
            //save loanPayment
        }
    }
}