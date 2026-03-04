using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinPro;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CategoriesController : ControllerBase
{
    private readonly AppDbContext _dbContext;
    private readonly CategoryService _categoryService;
    public CategoriesController(AppDbContext dbContext, CategoryService categoryService)
    {
        _dbContext = dbContext;
        _categoryService = categoryService;
    }

    [HttpGet]
    public IActionResult GetCategory()
    {
        var categories = _dbContext.Categories.Where(c => c.UserId == GetCurrentUser()).ToList();
        return Ok(categories);
    }

    [HttpGet("{id}")]
    public IActionResult GetCategoryById(int id)
    {
        var category = _dbContext.Categories.Where(c => c.UserId == GetCurrentUser()).FirstOrDefault(c => c.Id == id);
        if (category == null)
        {
            return NotFound(new { message = "category not found" });
        }
        return Ok(category);
    }

    [HttpPost]
    public IActionResult CreateCategory(CategoryRequest categoryRequest)
    {
		_categoryService.CreateNewCategory(categoryRequest, GetCurrentUser());
        return Ok(new { message = "Category Created" });
    }
    private int GetCurrentUser()
    {
        return Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    }

    //i'll implement later
    private bool VerifyIfCategoryTypeExists()
    {
        return true;
    }
}
