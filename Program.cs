using GameOfDrones.Data;
using GameOfDrones.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using dotenv.net;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddScoped<GameService>();

var allowedOrigins = builder.Configuration.GetValue<string>("AllowedOrigins");
var configuration = builder.Configuration;
DotEnv.Load();
var dbPath = Environment.GetEnvironmentVariable("DOTNET_DB_PATH");

var connectionString = configuration.GetConnectionString("DefaultConnection")
                                     .Replace("{DOTNET_DB_PATH}", dbPath);
builder.Services.AddDbContext<GameContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowConfiguredOrigin", builder =>
    {
        builder.WithOrigins(allowedOrigins)
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowConfiguredOrigin");

AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
AppContext.SetSwitch("System.Net.Http.EnableUnixDomainSocket", true);


app.MapControllers();
app.Run();
