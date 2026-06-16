using Microsoft.OpenApi;

using TodoApi.Api.Features.Tasks.Contracts;
using TodoApi.Api.Features.Tasks.Services;
using TodoApi.Api.Infrastructure.InMemory;
using TodoApi.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<ITaskRepository, InMemoryTaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();

// Swagger / OpenAPI
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Todo API",
        Description = "API REST para gestionar tareas",
        Version = "v1.0.0"
    });
});

var app = builder.Build();

// Error handling global — debe ser el primer middleware del pipeline
// para capturar excepciones de todos los middlewares posteriores.
app.UseMiddleware<ExceptionMiddleware>();

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

// Swagger UI — disponible en /swagger
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
