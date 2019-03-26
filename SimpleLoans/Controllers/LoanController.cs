using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SimpleLoans.Models;

namespace SimpleLoans.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
     // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Loan>> Get()
        {
            return new Loan[] { new Loan() { Amount = 10000, PaymentType = PaymentType.Weekly }};
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Loan> Get(int id)
        {
            return new Loan();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] Loan borrower)
        {
        }   
    }
}