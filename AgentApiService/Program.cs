
using AgentApiService.Models;
using AgentApiService.Services;
using AgentApiService.Repository;

var builder = WebApplication.CreateBuilder(args);

// Allow Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("DevOpen", policy =>
        policy
            .WithOrigins("http://127.0.0.1:5500") 
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()); 
});


// Bind configuration
builder.Services.Configure<OpenAIOptions>(
    builder.Configuration.GetSection("OpenAI"));

// Add service components 
builder.Services.AddMemoryCache();       
builder.Services.AddSingleton<LLMService>(); 
builder.Services.AddSingleton<ClientRepository>();
builder.Services.AddSingleton<ClientService>();

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


app.UseCors("DevOpen");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
