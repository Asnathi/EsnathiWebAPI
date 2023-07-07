namespace EsnathiWebAPI.Models
{
    public class Withdrawal
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int BankAccountId { get; set; }
        public BankAccount BankAccount { get; set; }
    }
}
