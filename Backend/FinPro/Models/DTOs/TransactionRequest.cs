namespace FinPro;
public class TransactionRequest
{
    public  decimal Amount { get; set; }
    public required string Description { get; set; }
    public DateTime Date { get; set; }
    public int CategoryId { get; set; } 
}
