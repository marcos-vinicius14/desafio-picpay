using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;
using Picpay_01.Models;

namespace Picpay_01.Services;

public class NotificationService
{
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _httpClientFactory;

    public NotificationService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
    {
        _configuration = configuration;
        _httpClientFactory = httpClientFactory;
    }
    public async Task SendNofitication(Users user, string message)
    {
        var email = user.Email;
        using HttpClient client = _httpClientFactory.CreateClient();
        
        //var notificationRequest = new NotificationDTO(email, message);
        var apiConfig = _configuration.GetSection("ApiConfig");
        var notificationUrl = apiConfig["SendNotification"];
        
        var notificationRequest = await client.GetAsync(notificationUrl);

        if (!notificationRequest.IsSuccessStatusCode)
            throw new Exception("Falha no envio de Email");
            
        
        
        var notificationResponseContent = await notificationRequest.Content.ReadAsStringAsync();
        var userContext = JsonConvert.DeserializeObject<NotificationModel>(notificationResponseContent);
        var messageValue = userContext.Message;
    }
}
