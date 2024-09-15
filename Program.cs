using Challenge_Locaweb.Models;
using Challenge_Locaweb.Services;
using Microsoft.AspNetCore.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<MongoDBSettingsModel>(
    builder.Configuration.GetSection("MongoDBSettings"));

// Registrando o serviço como singleton
//builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddSingleton<MessageService>();
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<PreferenceService>();


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
