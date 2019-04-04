using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleLoans.DataAccess.BogusRepository;
using SimpleLoans.DataAccess.Models;

namespace SimpleLoans.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowerController : ControllerBase
    {
        private readonly IBorrowerRepository _borrowerRepo;
        public BorrowerController(IBorrowerRepository borrowerRepo)
        {
          _borrowerRepo = borrowerRepo;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Borrower>> Get()
        {
            var r = _borrowerRepo.GetBorrowers();
            return r.ToList();
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
