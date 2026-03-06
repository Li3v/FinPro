using System.ComponentModel.Design;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace FinPro;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DashboardController : ControllerBase
{
    private readonly IDashboardService _dashboardService;
    public DashboardController(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    [HttpGet("summary")]
    public async Task<IActionResult> GetSummary(DateTime startDate, DateTime endDate)
    {
        var result = await _dashboardService
            .GetSummaryAsync(GetCurrentUser(), startDate, endDate);
        return Ok(result);
    }

    [HttpGet("categories")]
    public async Task<IActionResult> GetCategoryDistribution(DateTime startDate, DateTime endDate)
    {
        var result = await _dashboardService
            .GetCategoryDistributionAsync(GetCurrentUser(), startDate, endDate);

        return Ok(result);
    }

    [HttpGet("trend")]
    public async Task<IActionResult> GetTrend(DateTime startDate, DateTime endDate)
    {
        var result = await _dashboardService
            .GetTrendAsync(GetCurrentUser(), startDate, endDate);

        return Ok(result);
    }

    private int GetCurrentUser()
    {
        return Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    }
}
