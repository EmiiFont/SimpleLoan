using Bogus;
using SimpleLoans.DataAccess.Models;

namespace SimpleLoans.DataAccess.BogusRepository
{
    public interface ILoanRepository
    {
         Loan GetLoan();
    }

    public class LoanRepository : ILoanRepository
    {
        public Loan GetLoan()
        {
            var loan = new Faker<Loan>()
            .RuleFor(l => l.Amount, (f,u) => f.Random.Number(5000, 20000))
            .RuleFor(l => l.Rate, (f, k) => f.Random.Double(5,10))
            .RuleFor(l => l.Terms, (f, k) => f.Random.Number(1, 48))
            .RuleFor(l => l.CreationDate, (f, k)=> f.Date.Soon());

            return loan;
        }
    }
}