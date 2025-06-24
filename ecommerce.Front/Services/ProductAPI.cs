using ecommerce.API.DTOs.Product;
using ecommerce.API.DTOs.Response.Product;
using System.Net.Http.Json;
using System.Text.Json;

namespace ecommerce.Front.Services;

public class ProductAPI
{
    private readonly HttpClient _httpClient;

    public ProductAPI(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient("API");
    }

    public async Task<List<ProductResponseDTO>> GetAvailableProductsAsync()
    {
        try
        {
            var products = await _httpClient.GetFromJsonAsync<List<ProductResponseDTO>>("api/Product");
            
            return products?.Where(p => p.Stock.Quantity > 0).ToList() ?? new List<ProductResponseDTO>();
        }
        catch
        {
            return new List<ProductResponseDTO>();
        }
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

    public async Task<bool> PostProductAsync(ProductRequestDTO request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/Product", request);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Erro ao criar produto: {(int)response.StatusCode} - {response.ReasonPhrase}");
                Console.WriteLine($"Detalhes: {error}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exceção ao criar produto: {ex.Message}");
        }

        return false;
    }

    public async Task<bool> UpdateProductAsync(Guid id, ProductRequestDTO request)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Product/{id}", request);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao atualizar produto: {ex.Message}");
            return false;
        }
    }
    public async Task<bool> DeleteProductAsync(Guid id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/Product/{id}");
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao deletar produto: {ex.Message}");
            return false;
        }
    }

}
