using System.Net.Http;

var client = new HttpClient();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var customerApiUrl =
    Environment.GetEnvironmentVariable("CUSTOMER_API_URL")
    ?? "http://localhost:5001";

app.MapGet("/orders", async () =>
{
    using var client = new HttpClient();

    var customer = await client.GetStringAsync(
        $"{customerApiUrl}/customers/1"
    );

    return Results.Ok(new
    {
        Order = "Order-123",
        Customer = customer
    });
});

app.Run();