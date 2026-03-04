using System.ComponentModel.Design;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace FinPro;

[Authorize]
[ApiController]
[Route("dashboard")]
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
        var summary = await _dashboardService
            .GetSummaryAsync(GetCurrentUser(), startDate, endDate);
        return Ok();
    }

    [HttpGet("category-distribution")]
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
