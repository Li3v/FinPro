using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace FinPro;

[Index(nameof(Email), IsUnique = true)]
public class User
{
    public int Id { get; set; }
    public required string Name { get; set; }
    [Required]
    public required string Email { get; set; }
    [Required]
    public required string PasswordHash { get; set; }
    public DateTime CreatedAt { get; set; }
 //   public ICollection<Category> Category { get; set; } = new List<Category>();
}
