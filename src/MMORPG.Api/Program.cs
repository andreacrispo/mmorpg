using MMORPG.Infrastrutture;
using MMORPG.Application;
using Api.Hub;
using Api.Settings;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<RealTimeSettings>(builder.Configuration.GetSection(RealTimeSettings.RealTime));

builder.Services.AddInfrastructure();
builder.Services.AddDependencyInjection();

builder.Services.AddHostedService<CharacterBackgroundService>();

builder.Services.AddSignalR();


WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<CharacterHub>("ws-character-hub");

app.Run();



//For Integration Test Reference
public partial class Program { }

