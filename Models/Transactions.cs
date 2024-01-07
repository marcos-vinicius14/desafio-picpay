namespace Picpay_01.Models;

public class Transactions
{
    public int Id { get; set; }
    public double Amount { get; set; } = 0.0;
    public Users Sender { get; set; }
    public Users Receiver { get; set; }
    public DateTime LocalDateTime { get; set; }

}