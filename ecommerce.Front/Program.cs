using ecommerce.Front;
using ecommerce.Front.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<AuthenticationStateProvider, AuthAPI>();
builder.Services.AddScoped(sp => (AuthAPI)sp.GetRequiredService<AuthenticationStateProvider>());

builder.Services.AddHttpClient("API", client => {
    client.BaseAddress = new Uri(builder.Configuration["APIServer:Url"]!);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
}).AddHttpMessageHandler<CookieHandler>();

builder.Services.AddScoped<CookieHandler>();
builder.Services.AddScoped<AuthAPI>();
builder.Services.AddScoped<ProductAPI>();
builder.Services.AddScoped<OrderAPI>();

builder.Services.AddAuthorizationCore();
builder.Services.AddMudServices();

await builder.Build().RunAsync();
