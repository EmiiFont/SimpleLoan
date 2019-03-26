using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleLoans.Models;

namespace SimpleLoans.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowerController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Borrower>> Get()
        {
            return new Borrower[] { new Borrower() { Name = "John", Identification = "000-0000000-0"} };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Borrower> Get(int id)
        {
            return new Borrower();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] Borrower borrower)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Borrower borrower)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
