using System.Collections.Generic;
using System.Linq;
using Bogus;
using SimpleLoans.DataAccess.Models;

namespace SimpleLoans.DataAccess.BogusRepository
{
    public interface IBorrowerRepository
    {
         IEnumerable<Borrower> GetBorrowers();
    }

    public class BorrowerRepository : IBorrowerRepository
    {
        public IEnumerable<Borrower> GetBorrowers()
        {
            var address = new Faker<Address>()
            .RuleFor(add => add.City, (f, u) => f.Address.City())
            .RuleFor(add => add.Street, (f,u) => f.Address.StreetName())
            .RuleFor(add => add.ApartmentNumber, (f, u) => f.Random.Number(1, 300))
            .RuleFor(add => add.Province, (f, u) => f.Address.County());
           
            
            var borrowers = new Faker<Borrower>()
            .RuleFor(bw => bw.Name, (f, u) => f.Name.FirstName())
            .RuleFor(bw => bw.Identification, (f, u) => f.Vehicle.ToString())
            .RuleFor(bw => bw.Address, (f, u) => address);
        
             return borrowers.Generate(10);
        }
    }
}