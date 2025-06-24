using ecommerce.API.DTOs.Response.Order;
using System.Net.Http.Json;

namespace ecommerce.Front.Services;

public class OrderAPI
{
    private readonly HttpClient _httpClient;

    public OrderAPI(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient("API");
    }

    public async Task<List<OrderResponseDTO>> GetOrdersAsync()
    {
        try
        {
            var orders = await _httpClient.GetFromJsonAsync<List<OrderResponseDTO>>("api/Order");
            return orders ?? new List<OrderResponseDTO>();
        }
        catch
        {
            return new List<OrderResponseDTO>();
        }
    }

    public async Task<bool> DeleteOrderAsync(Guid id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/Order/{id}");
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting order: {ex.Message}");
            return false;
        }
    }
}
