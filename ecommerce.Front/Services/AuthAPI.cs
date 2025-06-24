using ecommerce.API.DTOs.Request.Auth;
using ecommerce.Shared.DTOs.Response.NovaPasta;
using ecommerce.Shared.DTOs.Response.User;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;

namespace ecommerce.Front.Services;

public class AuthAPI(IHttpClientFactory factory) : AuthenticationStateProvider
{
    private bool autenticado = false;
    private readonly HttpClient _httpClient = factory.CreateClient("API");

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        autenticado = false;
        var pessoa = new ClaimsPrincipal();
        var response = await _httpClient.GetAsync("auth/manage/info");

        if (response.IsSuccessStatusCode)
        {
            var info = await response.Content.ReadFromJsonAsync<UserResponse>();
            Claim[] dados =
            [
                new Claim(ClaimTypes.Name, info.Email),
                new Claim(ClaimTypes.Email, info.Email)
            ];

            var identity = new ClaimsIdentity(dados, "Cookies");
            pessoa = new ClaimsPrincipal(identity);
            autenticado = true;
        }

        return new AuthenticationState(pessoa);
    }

    public async Task<AuthResponse> LoginAsync(string email, string senha)
    {
        var response = await _httpClient.PostAsJsonAsync("auth/login?useCookies=true", new
        {
            email,
            password = senha
        });

        if (response.IsSuccessStatusCode)
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            return new AuthResponse { Sucesso = true };
        }

        return new AuthResponse { Sucesso = false, Erros = ["Invalid login/password"] };
    }
    public async Task<AuthResponse> RegisterAsync(string? email, string? password)
    {
        try
        {
            var registerData = new RegisterRequestDTO(email ?? "", password ?? "");
            var response = await _httpClient.PostAsJsonAsync("auth/register", registerData);

            if (response.IsSuccessStatusCode)
            {
                if (response.Content.Headers.ContentLength == 0 ||
                    (response.Content.Headers.ContentType != null && !response.Content.Headers.ContentType.MediaType!.Contains("json")))
                {
                    return new AuthResponse { Sucesso = true, Erros = Array.Empty<string>() };
                }
                try
                {
                    var authResponse = await response.Content.ReadFromJsonAsync<AuthResponse>();
                    return authResponse ?? new AuthResponse { Sucesso = false, Erros = new string[] { "Empty or invalid JSON response from API after successful status." } };
                }
                catch (JsonException jsonEx)
                {
                    Console.Error.WriteLine($"JSON Deserialization Error on successful response: {jsonEx.Message}");
                    return new AuthResponse { Sucesso = false, Erros = new string[] { $"Registration failed: Invalid JSON format in success response. Details: {jsonEx.Message}" } };
                }
            }
            else
            {
                string errorContent = "No error content received.";
                try
                {
                    errorContent = await response.Content.ReadAsStringAsync();
                }
                catch {}

                AuthResponse? authResponseFromError = null;
                try
                {
                    authResponseFromError = JsonSerializer.Deserialize<AuthResponse>(errorContent);
                }
                catch {}

                if (authResponseFromError != null && authResponseFromError.Erros != null && authResponseFromError.Erros.Any())
                {
                    return authResponseFromError;
                }
                return new AuthResponse { Sucesso = false, Erros = new string[] { $"Registration failed. Status: {response.StatusCode}. Details: {errorContent}" } };
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"AuthAPI RegisterAsync Exception: {ex.Message}");
            return new AuthResponse { Sucesso = false, Erros = new string[] { $"Communication error: {ex.Message}" } };
        }
    }

    public async Task LogoutAsync()
    {
        await _httpClient.PostAsync("auth/logout", null);
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public async Task<bool> VerificaAutenticado()
    {
        await GetAuthenticationStateAsync();
        return autenticado;
    }
}