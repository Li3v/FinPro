using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace FinPro;

[Index(nameof(UserId), nameof(Name), IsUnique = true)]
public class Category
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    [MaxLength(150)]
    public required string Name { get; set; }
    public required int Type { get; set; }
    public List<Transaction> Transaction { get; set; } = new List<Transaction>();
}
