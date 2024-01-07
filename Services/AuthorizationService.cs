namespace Picpay_01.Services;

public class AuthorizationService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public AuthorizationService(IHttpClientFactory httpClientFactory)
        => _httpClientFactory = httpClientFactory;

}