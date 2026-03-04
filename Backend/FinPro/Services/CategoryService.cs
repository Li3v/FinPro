using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FinPro;

public class CategoryService
{
    private readonly AppDbContext _dbContext;
    public CategoryService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Category CreateNewCategory(CategoryRequest request, int id)
    {
        var category = new Category
        {
            Name = request.Name,
            Type = Convert.ToInt32(request.Type),
            UserId = id,
        };
        _dbContext.Add(category);
        _dbContext.SaveChanges();
        return category;
    }
}
