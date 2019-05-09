namespace SimpleLoans.DataAccess.Models
{
    public class User
    {
        public string FirstName { get; set; }
        public string  LastName { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; internal set; }
        public byte[] PasswordSalt { get; set; }
    }
}