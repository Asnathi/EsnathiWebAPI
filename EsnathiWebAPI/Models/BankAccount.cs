namespace EsnathiWebAPI.Models
{
    public class BankAccount
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public decimal AvailableBalance { get; set; }
        public int AccountHolderId { get; set; }
        public AccountHolder AccountHolder { get; set; }
    }
}
