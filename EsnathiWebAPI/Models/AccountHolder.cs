namespace EsnathiWebAPI.Models
{
    public class AccountHolder
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        // Other properties like DateOfBirth, IDNumber, Address, MobileNumber, Email, etc.
        public List<BankAccount> BankAccounts { get; set; }
    }
}
