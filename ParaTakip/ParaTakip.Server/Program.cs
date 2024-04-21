using log4net.Config;
using ParaTakip.Configuration;
using ParaTakip.Core;
using ParaTakip.PythonIntegrator;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Configurations.SetConfigurations(builder.Configuration);
Configurations.ConfigureServices(builder.Services);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();

builder.Logging.ClearProviders();
builder.Logging.AddLog4Net();

var app = builder.Build();

AppServiceProvider.Instance.RegisterAsSingleton(typeof(IApplicationContext), app.Services.GetService<IApplicationContext>());

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

Configurations.RegisterServices();
Configurations.RegisterBusinessServices();
Configurations.RegisterDataAccessServices();
Configurations.LoadParameterCache();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();

app.UseCookiePolicy();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
