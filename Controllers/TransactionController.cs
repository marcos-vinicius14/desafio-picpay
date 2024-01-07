using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Picpay_01.Data;
using Picpay_01.Models;
using Picpay_01.Services;
using Picpay_01.ViewModels;

namespace Picpay_01.Controllers;

[ApiController]
public class TransactionController : ControllerBase
{
    private readonly TransactionService _transactionService;
    private readonly IMemoryCache _cache;

    public TransactionController(
        TransactionService transactionService, 
        IMemoryCache cache
        )
    {
        _transactionService = transactionService;
        _cache = cache;
    }

    [HttpGet("v1/transactions")]
    public async Task<IActionResult> GetTransactionsAsync(
        [FromServices] DataContext context)
    {
        try
        {
            var transactions = await _cache.GetOrCreate("TransactionsCache", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
                return await _transactionService.GetTransactionsAsync(context);
            });

            return Ok(new ResulvViewModel<List<Transactions>>(transactions));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResulvViewModel<List<Transactions>>("Falha interna no servidor"));
        }
    }

    [HttpPost("v1/transactions")]
    public async Task<IActionResult> CreateTransactionAsync(
        [FromBody] TransactionViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        try
        {
            var newTransactions = await _transactionService.CreateTransaction(model);
            
            Console.WriteLine($"Dados da transação: { newTransactions }");

            if (newTransactions == null)
                return NotFound();
            
            //_notificationService.SendNofitication()

            return Created($"v1/transactions/{newTransactions.Id}", newTransactions);
        }
        catch 
        {
            return StatusCode(500, new ResulvViewModel<TransactionViewModel>($"Falha interna no servidor"));
        }
    }

}