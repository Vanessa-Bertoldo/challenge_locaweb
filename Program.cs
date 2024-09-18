using AutoMapper;
using Challenge_Locaweb.Models;
using Challenge_Locaweb.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Adicione serviços ao contêiner.
builder.Services.Configure<MongoDBSettingsModel>(
    builder.Configuration.GetSection("MongoDBSettings"));

// Configuração do AutoMapper
//builder.Services.AddAutoMapper(typeof(MessageMongoModel));

// Registro dos serviços
builder.Services.AddSingleton<MessageService>();
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<PreferenceService>();

// Adicione controladores
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ContractResolver = new DefaultContractResolver();
    });

// Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Challenge Locaweb API",
        Version = "v1"
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Challenge Locaweb API V1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
