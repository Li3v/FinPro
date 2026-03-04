using System.ComponentModel.DataAnnotations;

namespace FinPro;

public class Transaction
{
    public int Id { get; set; }
    //public int UserId { get; set; }
    public int CategoryId { get; set; }
    public decimal Amount { get; set; }
    [MaxLength(250)]
    public required string Description { get; set; }
    public DateTime Date { get; set; }
    public Category Category { get; set; } = null!;
}
