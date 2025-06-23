using ecommerce.API.DTOs.Response.Product;
using System.Text.Json;

namespace ecommerce.Front.Services;

public class ProductAPI
{
    private readonly HttpClient _httpClient;

    public ProductAPI(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient("API");
    }

    public async Task<List<ProductResponseDTO>?> GetProductsAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("api/Product");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<ProductResponseDTO>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Erro HTTP: {(int)response.StatusCode} - {response.ReasonPhrase}");
                Console.WriteLine($"Conteúdo do erro: {error}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao buscar produtos: {ex.Message}");
        }

        return null;
    }
}
