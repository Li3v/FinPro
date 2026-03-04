namespace FinPro;

public interface IDashboardService
{
    Task<SummaryDto> GetSummaryAsync(int userId, DateTime startDate, DateTime endDate);
    Task<List<CategoryDto>> GetCategoryDistributionAsync(int userId, DateTime startDate, DateTime endDate);
    Task<List<TrendDto>> GetTrendAsync(int userId, DateTime startDate, DateTime endDate);
}
