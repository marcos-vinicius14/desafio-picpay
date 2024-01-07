using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Picpay_01.Data;
using Picpay_01.Models;
using Picpay_01.ViewModels;
using Exception = System.Exception;

namespace Picpay_01.Services;

public class TransactionService
{
    private readonly UserService _userService;
    private readonly DataContext _context;
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _httpClient;
    private readonly NotificationService _notificationService;

    public TransactionService(
        IConfiguration configuration, 
        IHttpClientFactory httpClient, 
        NotificationService notificationService, 
        UserService userService,
        DataContext context)
    {
        _configuration = configuration;
        _httpClient = httpClient;
        _notificationService = notificationService;
        _userService = userService;
        _context = context;

    }


    public async Task<Transactions> CreateTransaction(TransactionViewModel model)
    {
        Users sender = await _userService.FindUserById(model.SenderId);
        Users receiver = await _userService.FindUserById(model.ReceiverId);

        if (sender == null || receiver == null)
           throw new Exception("Usuarios não existem");
        
        _userService.ValidateTransaction(sender, model.Value);
        _userService.ValidateTransaction(receiver, model.Value);

        if (await CheckAuthorization(sender, model.Value) == false)
            throw new Exception("Transação não autorizada");

        try
        {
            var transactions = new Transactions
            {
                Amount = model.Value,
                Sender = sender,
                Receiver = receiver,
            };

            sender.Balance -= model.Value;
            receiver.Balance += model.Value;

            
            await _notificationService.SendNofitication(sender, "Transação realizada com sucesso");
            await _notificationService.SendNofitication(receiver, $"Você acaba de receber uma nova transação");

            await _context.Transactions.AddAsync(transactions);
            await _context.SaveChangesAsync();
            
            return transactions;
        }
        catch (Exception e)
        {
            throw new Exception($"Não foi possível concluir a transação. Tente novamente mais tarde - { e.Message } ");
        }
    }

    public async Task<List<Transactions>> GetTransactionsAsync(DataContext context)
    {
        return await context
            .Transactions
            .AsNoTracking()
            .ToListAsync();
    }
    private async Task<bool> CheckAuthorization(Users sender, double value)
    {
        using HttpClient httpClient = _httpClient.CreateClient();
        
        var apiConfig = _configuration.GetSection("ApiConfig");
        var checkAuthorizationUrl = apiConfig["CheckAuthorization"];
        
        var httpResponseMessage = await httpClient.GetAsync(checkAuthorizationUrl);

        if (!httpResponseMessage.IsSuccessStatusCode)
            throw new InvalidOperationException("Falha na autorização. Tente novamente mais tarde");

        try
        {
            var responseContent = await httpResponseMessage.Content.ReadAsStringAsync();

            var isAuthorized = JsonConvert.DeserializeObject<CheckAuthorizationModel>(responseContent);

            if ( isAuthorized != null && isAuthorized.Message.Equals("Autorizado")  )
                return true;
        }
        catch (HttpRequestException e)
        {
            throw new InvalidOperationException("Falha durante a solicitação da autorização. Tente novamente mais tarde.");
        }
        catch (TaskCanceledException e)
        {
            throw new InvalidOperationException("A solicitação expirou. Tente novamente mais tarde.");
        }
        catch (Exception e)
        {
            throw new InvalidOperationException("Falha interna no servidor.");
        }

        return false;
    }
    
}