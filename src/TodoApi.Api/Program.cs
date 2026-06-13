using TodoApi.Api.Features.Tasks.Contracts;
using TodoApi.Api.Features.Tasks.Services;
using TodoApi.Api.Infrastructure.InMemory;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<ITaskRepository, InMemoryTaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
