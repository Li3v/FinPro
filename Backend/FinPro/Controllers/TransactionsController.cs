using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinPro;

[ApiController]
[Route("transaction")]
[Authorize]
public class TransactionsController : ControllerBase
{
    private readonly AppDbContext _dbContext;
    private readonly TransactionService _transactionService;

    public TransactionsController(AppDbContext dbContext, TransactionService transactionService)
    {
        _dbContext = dbContext;
        _transactionService = transactionService;
    }

    [HttpGet]
    [Route("Get")]
    public IActionResult GetAllTransactions()
    {
        return Ok(_dbContext.Transactions.ToList());
    }

    [HttpGet]
    [Route("Get/{id}")]
    public IActionResult GetTransactionById(int id)
    {
        var user = _dbContext.Transactions.FirstOrDefault(t => t.Id == id);
        if (user == null)
        {
            return NotFound(new { message = "Transaction not found" });
        }
        return Ok(user);
    }

    [HttpPost]
    [Route("Post")]
    public IActionResult CreateTransaction(TransactionRequest transactionRequest)
    {
        _transactionService.CreateNewTransaction(transactionRequest, GetCurrentUser());
        return Ok(new { message = "Transaction created" });
    }

    [HttpDelete]
    [Route("Delete/{id}")]
    public IActionResult DeleteTransaction(int id)
    {
        var transaction = _dbContext.Transactions.FirstOrDefault(t => t.Id == id);
        if (transaction == null)
        {
            return NotFound(new { message = "transaction not found" });
        }
        _dbContext.Transactions.Remove(transaction);
        _dbContext.SaveChanges();
        return Ok(new { message = "transaction deleted" });
    }

    private int GetCurrentUser()
    {
        return Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    }
}
