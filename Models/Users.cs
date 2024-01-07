using Picpay_01.Models.Enums;

namespace Picpay_01.Models;

public class Users
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Document { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public double Balance { get; set; } = 0.0;
    
    //public IList<Transactions> TransactionsSender { get; }
    //public IList<Transactions> TransactionsReceiver { get; }
    public UserType UserType { get; set; }

}