using ecommerce.Domain.Interfaces.Repository;
using ecommerce.Domain.Interfaces.Repository.DbConfig;
using ecommerce.Domain.Interfaces.Services;
using ecommerce.Domain.Interfaces.Services.DbConfig;
using ecommerce.Domain.Services;
using ecommerce.Domain.Services.DbConfig;
using ecommerce.Infra.Context;
using ecommerce.Infra.Repository;
using ecommerce.Infra.Repository.DbConfig;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>((options) => {
    options
            .UseSqlServer(builder.Configuration["ConnectionStrings:Ecommerce"])
            .UseLazyLoadingProxies();
});

builder.Services
    .AddIdentityApiEndpoints<ApplicationUser>()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderProductService, OrderProductService>();
builder.Services.AddScoped<IOrderProductRepository,  OrderProductRepository>();
builder.Services.AddScoped<IProductStockRepository, ProductStockRepository>();
builder.Services.AddScoped<IProductStockService, ProductStockService>();

builder.Services.AddScoped(typeof(IRepositoryBaseConfig<>), typeof(RepositoryBaseConfig<>));
builder.Services.AddScoped(typeof(IServiceBaseConfig<>), typeof(ServiceBaseConfig<>));

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services
    .AddControllers()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontendDev", policy =>
    {
        policy.WithOrigins("https://localhost:7096")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowFrontendDev");

app.UseAuthorization();

app.MapControllers();

app.MapGroup("auth").MapIdentityApi<ApplicationUser>().WithTags("Auth");

app.MapPost("auth/logout", async ([FromServices] SignInManager<ApplicationUser> signInManager) =>
{
    await signInManager.SignOutAsync();
    return Results.Ok();
}).RequireAuthorization().WithTags("Auth");


app.Run();
