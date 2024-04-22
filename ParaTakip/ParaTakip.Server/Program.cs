using JsonSubTypes;
using ParaTakip.Configuration;
using ParaTakip.Core;
using ParaTakip.Entities.Enums;
using static ParaTakip.Entities.Wealth;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Configurations.SetConfigurations(builder.Configuration);
Configurations.ConfigureServices(builder.Services);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    //Register the subtypes of the Device (Phone and Laptop)
    //and define the device Discriminator
    options.SerializerSettings.Converters.Add(
        JsonSubtypesConverterBuilder
        .Of(typeof(BaseWealthValue), "BaseWealthValueDiscriminator")
        .RegisterSubtype(typeof(ForeignExchangeAndPreciousMetals), WealthType.FOREIGN_EXCHANGE_AND_PRECIOUS_METALS)
        .RegisterSubtype(typeof(StockTrading), WealthType.STOCK_TRADING)
        .RegisterSubtype(typeof(FundTrading), WealthType.FUND_TRADING)
        .SerializeDiscriminatorProperty()
        .Build()
    );
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.UseAllOfToExtendReferenceSchemas();
    c.UseAllOfForInheritance();
    c.UseOneOfForPolymorphism();
    c.SelectDiscriminatorNameUsing(type =>
    {
        return type.Name switch
        {
            nameof(BaseWealthValue) => "BaseWealthValueDiscriminator",
            _ => null
        };
    });
});
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
