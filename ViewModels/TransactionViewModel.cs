using System.ComponentModel.DataAnnotations;
using Picpay_01.Models;

namespace Picpay_01.ViewModels;

public class TransactionViewModel
{
    [Required(ErrorMessage = "ID é obrigatório!")]
    public int SenderId { get; set; }
    
    [Required(ErrorMessage = "ID é obrigatório!")]
    public int ReceiverId { get; set; }
    
    [Required(ErrorMessage = "É necessário um valor maior que 0 para realizar uma transação!")]
    public double Value { get; set; }
}