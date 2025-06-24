using ecommerce.API.DTOs.Request.Order;
using ecommerce.API.DTOs.Response.Order;
using MudBlazor;
using System.Net.Http.Json;

namespace ecommerce.Front.Services;

public class OrderAPI
{
    private readonly HttpClient _httpClient;
    private readonly ISnackbar _snackbar;

    public OrderAPI(IHttpClientFactory factory, ISnackbar snackbar)
    {
        _httpClient = factory.CreateClient("API");
        _snackbar = snackbar;
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

    public async Task<bool> PostOrderAsync(OrderRequestDTO orderDto)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/Order", orderDto);

            if (response.IsSuccessStatusCode)
            {
                _snackbar.Add("Order created successfully!", Severity.Success);
                return true;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error creating order: {response.StatusCode} - {errorContent}");
                _snackbar.Add($"Failed to create order: {errorContent}", Severity.Error);
                return false;
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Network error creating order: {ex.Message}");
            _snackbar.Add($"Network error: {ex.Message}", Severity.Error);
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            _snackbar.Add($"An unexpected error occurred: {ex.Message}", Severity.Error);
            return false;
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
