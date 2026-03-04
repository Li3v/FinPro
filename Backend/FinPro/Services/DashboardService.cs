using Microsoft.EntityFrameworkCore;

namespace FinPro;

public class DashboardService //: IDashboardService
{
    private readonly AppDbContext _dbContext;
    public DashboardService(AppDbContext dbContext) { _dbContext = dbContext; }


    public async Task<SummaryDto> GetSummaryAsync(int userId, DateTime startDate, DateTime endDate)
    {
        var transactions = await _dbContext.Transactions
        .Include(t => t.CategoryId)
        .Where(t =>
            t.Category.UserId == userId &&
            t.Date >= startDate &&
            t.Date <= endDate)
            .ToListAsync();

        var totalIncome = transactions
            .Where(t => t.Category.Type == 1)
            .Sum(t => t.Amount);

        var totalExpense = transactions
            .Where(t => t.Category.Type == 0)
            .Sum(t => t.Amount);

        return new SummaryDto
        {
            totalIncome = totalIncome,
            TotalExpense = totalExpense
        };
    }


    public async Task<List<CategoryDto>> GetCategoryDistributionAsync(int userId, DateTime startDate, DateTime endDate)
    {
        return await _dbContext.Transactions
            .Include(t => t.CategoryId)
            .Where(t =>
                t.Category.UserId == userId &&
                t.Date >= startDate &&
                t.Date <= endDate &&
                t.Category.Type == 0)
            .GroupBy(t => t.Category.Name)
            .Select(g => new CategoryDto
            {
                CategoryName = g.Key,
                Total = g.Sum(x => x.Amount)
            })
            .ToListAsync();
    }

     public async Task<List<TrendDto>> GetTrendAsync(int userId, DateTime startDate, DateTime endDate)
     {
         return await _dbContext.Transactions
             .Include(t => t.CategoryId)
             .Where(t =>
                 t.Category.UserId == userId &&
                 t.Date >= startDate &&
                 t.Date <= endDate)
             .GroupBy(t => new { t.Date.Year, t.Date.Month })
             .Select(g => new TrendDto
             {
                 Year = g.Key.Year,
                 Month = g.Key.Month,
                 Income = g.Where(t => t.Category.Type == 1).Sum(t => t.Amount),
                 Expense = g.Where(t => t.Category.Type == 0).Sum(t => t.Amount)
             })
             .OrderBy(x => x.Year)
             .ThenBy(x => x.Month)
             .ToListAsync();
     }

}
