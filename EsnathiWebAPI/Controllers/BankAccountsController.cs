using EsnathiWebAPI.Data;
using EsnathiWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EsnathiWebAPI.Controllers
{
    [Route("api/bank-accounts")]
    [ApiController]
    [Authorize]
    public class BankAccountsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BankAccountsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{accountNumber}")]
        public ActionResult<BankAccount> GetBankAccount(string accountNumber)
        {
            var bankAccount = _context.BankAccounts.FirstOrDefault(a => a.AccountNumber == accountNumber);

            if (bankAccount == null)
                return NotFound();

            return bankAccount;
        }

        [HttpPost("{accountNumber}/withdrawals")]
        public ActionResult CreateWithdrawal(string accountNumber, [FromBody] Withdrawal withdrawal)
        {
            var bankAccount = _context.BankAccounts.FirstOrDefault(a => a.AccountNumber == accountNumber);

            if (bankAccount == null)
                return NotFound();

            if (withdrawal.Amount <= 0)
                return BadRequest("Withdrawal amount must be greater than 0.");

            if (withdrawal.Amount > bankAccount.AvailableBalance)
                return BadRequest("Withdrawal amount exceeds the available balance.");

            bankAccount.AvailableBalance -= withdrawal.Amount;
            _context.Withdrawals.Add(withdrawal);
            _context.SaveChanges();

            return Ok();
        }
    }
}

