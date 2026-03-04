using Microsoft.AspNetCore.Mvc;

namespace FinPro;

public class TransactionService
{
    private readonly AppDbContext _dbContext;
    public TransactionService(AppDbContext dbContext) { _dbContext = dbContext; }
    public Transaction CreateNewTransaction(TransactionRequest request, int id)
    {
        var transaction = new Transaction
        {
            Amount = request.Amount,
            Description = request.Description,
            Date = DateTime.Now,
            CategoryId = request.CategoryId
        };
        _dbContext.Add(transaction);
        _dbContext.SaveChanges();
        return transaction;
    }
}
