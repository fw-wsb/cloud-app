using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.Models;
using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);

// 1. Podłączenie do Key Vault (działa tylko w chmurze)
if (!builder.Environment.IsDevelopment())
{
    var keyVaultEndpoint = new Uri("https://kv-filip-95747.vault.azure.net/");
    builder.Configuration.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential());
}

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 2. Pobieranie hasła z Key Vaulta zamiast z pliku
// Używamy nazwy klucza "DbConnectionString", którą nadałeś w Azure Portal
var connectionString = builder.Configuration["DbConnectionString"];

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddSingleton<List<TaskItem>>(new List<TaskItem>());

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseAuthorization();
app.MapControllers();

app.Run();A