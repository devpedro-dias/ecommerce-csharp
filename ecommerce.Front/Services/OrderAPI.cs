namespace ecommerce.Front.Services;

public class OrderAPI
{
    private readonly HttpClient _httpClient;

    public OrderAPI(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient("API");
    }
}
