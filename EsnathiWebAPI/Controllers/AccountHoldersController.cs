using EsnathiWebAPI.Data;
using EsnathiWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EsnathiWebAPI.Controllers
{
    [Route("api/account-holders")]
    [ApiController]
    [Authorize]
    public class AccountHoldersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AccountHoldersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}/bank-accounts")]
        public ActionResult<List<BankAccount>> GetBankAccounts(int id)
        {
            var accountHolder = _context.AccountHolders
                .Include(a => a.BankAccounts)
                .FirstOrDefault(a => a.Id == id);

            if (accountHolder == null)
                return NotFound();

            return accountHolder.BankAccounts;
        }
    }
}
