using GameOfDrones.Data;
using GameOfDrones.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddDbContext<GameContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<GameService>();

builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowLocalhost4200",
			policy => policy.WithOrigins("http://localhost:4200")
											.AllowAnyMethod()
											.AllowAnyHeader());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//TODO:parametrizar
app.UseCors("AllowLocalhost4200");

AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
AppContext.SetSwitch("System.Net.Http.EnableUnixDomainSocket", true);


app.MapControllers();
app.Run();
