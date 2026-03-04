using Microsoft.EntityFrameworkCore;

namespace FinPro;

[Index(nameof(CategoryId), nameof(Period), IsUnique = true)]
public class Budget
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public required double LimitAmount { get; set; }
    public DateTime Period { get; set; }
    // public Category Category { get; set; } = null!;
}
