using System.Collections.Generic;

namespace SimpleLoans.DataAccess.Models
{
    public class AccountInformation
    {
        public string CompanyName { get; set; }
        public string RegistrationNumber { get; set; }
        public IEnumerable<User> People { get; set; }
    }
}