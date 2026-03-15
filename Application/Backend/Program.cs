using Backend.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Mock bazy danych w pamięci na potrzeby zadania
var tasks = new List<TaskItem>
{
    new TaskItem { Id = 1, Name = "Zadanie 1", IsCompleted = false },
    new TaskItem { Id = 2, Name = "Zadanie 2", IsCompleted = true }
};

builder.Services.AddSingleton(tasks);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

app.Run();