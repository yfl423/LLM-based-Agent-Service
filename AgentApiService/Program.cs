
using AgentApiService.Models;
using AgentApiService.Services;

var builder = WebApplication.CreateBuilder(args);

// Bind configuration
builder.Services.Configure<OpenAIOptions>(
    builder.Configuration.GetSection("OpenAI"));

// Add service components 
builder.Services.AddMemoryCache();       
builder.Services.AddSingleton<LLMService>(); 


// Add controller components.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
