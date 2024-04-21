using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ParaTakip.Business.Caches;
using ParaTakip.Business.Interfaces;
using ParaTakip.Business.Services;
using ParaTakip.Core;
using ParaTakip.DataAccess.Interfaces;
using ParaTakip.DataAccess.Services;
using System.Text;

namespace ParaTakip.Configuration
{
    public class Configurations
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddTransient<IApplicationContext, ApplicationContext>();

            if (System.Reflection.Assembly.GetEntryAssembly()?.GetName()?.Name == "ParaTakip.Server")
            {
                ConfigurationCache.Instance.TryGet<string>("JwtSecret", out string jwtSecretKey);
                var key = Encoding.ASCII.GetBytes(jwtSecretKey);
                services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                }).AddCookie();
            }
            else
            {
                services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie(options =>
               {
                   options.LoginPath = "/User/Login";
                   options.AccessDeniedPath = "/User/AccessDenied";
                   options.LogoutPath = "/User/Logout";
               });
            }
        }

        public static void SetConfigurations(IConfiguration configuration)
        {
            //DB
            ConfigurationCache.Instance.Add("MongoDbConnectionString", configuration.GetSection("MongoConnection:ConnectionString").Value);
            ConfigurationCache.Instance.Add("MongoDbName", configuration.GetSection("MongoConnection:Database").Value);

            //API
            ConfigurationCache.Instance.Add("JwtSecret", configuration.GetSection("AppSettings:JwtSecret").Value);
            ConfigurationCache.Instance.Add("WC_API_KEY", configuration.GetSection("AppSettings:WC_API_KEY").Value);
            ConfigurationCache.Instance.Add("WC_API_SECRET", configuration.GetSection("AppSettings:WC_API_SECRET").Value);
            ConfigurationCache.Instance.Add("SHOPIER_PAT", configuration.GetSection("AppSettings:SHOPIER_PAT").Value);
            ConfigurationCache.Instance.Add("BATCH_API_KEY", configuration.GetSection("AppSettings:BATCH_API_KEY").Value);

            ConfigurationCache.Instance.Add("TRENDYOL_SELLER_ID", configuration.GetSection("AppSettings:TRENDYOL_SELLER_ID").Value);
            ConfigurationCache.Instance.Add("TRENDYOL_API_ADRESS", configuration.GetSection("AppSettings:TRENDYOL_API_ADRESS").Value);
            ConfigurationCache.Instance.Add("TRENDYOL_USER_AGENT", configuration.GetSection("AppSettings:TRENDYOL_USER_AGENT").Value);
            ConfigurationCache.Instance.Add("TRENDYOL_TOKEN", configuration.GetSection("AppSettings:TRENDYOL_TOKEN").Value);


            ConfigurationCache.Instance.Add("MysqlDB:Server", configuration.GetSection("MysqlDB:Server").Value);
            ConfigurationCache.Instance.Add("MysqlDB:Port", configuration.GetSection("MysqlDB:Port").Value);
            ConfigurationCache.Instance.Add("MysqlDB:Uid", configuration.GetSection("MysqlDB:Uid").Value);
            ConfigurationCache.Instance.Add("MysqlDB:Pwd", configuration.GetSection("MysqlDB:Pwd").Value);
            ConfigurationCache.Instance.Add("MysqlDB:Database", configuration.GetSection("MysqlDB:Database").Value);

            ConfigurationCache.Instance.Add("EmailReportConfigs:Host", configuration.GetSection("EmailReportConfigs:Host").Value);
            ConfigurationCache.Instance.Add("EmailReportConfigs:Port", configuration.GetSection("EmailReportConfigs:Port").Value);
            ConfigurationCache.Instance.Add("EmailReportConfigs:Username", configuration.GetSection("EmailReportConfigs:Username").Value);
            ConfigurationCache.Instance.Add("EmailReportConfigs:Password", configuration.GetSection("EmailReportConfigs:Password").Value);
            ConfigurationCache.Instance.Add("EmailReportConfigs:From", configuration.GetSection("EmailReportConfigs:From").Value);
            ConfigurationCache.Instance.Add("EmailReportConfigs:To", configuration.GetSection("EmailReportConfigs:To").Value);
        }

        public static void RegisterServices()
        {
            AppServiceProvider.Instance.RegisterAsSingleton(typeof(IMapper), AutoMapperBuilder.GetMapper());
            AppServiceProvider.Instance.RegisterAsSingleton(typeof(IMemoryCache), new MemoryCache(new MemoryCacheOptions()
            {
                ExpirationScanFrequency = TimeSpan.FromHours(1)
            }));
        }

        public static void RegisterBusinessServices()
        {
            
 			AppServiceProvider.Instance.Register(typeof(IAppUserService), new AppUserService());
 			AppServiceProvider.Instance.Register(typeof(IEmailTemplateService), new EmailTemplateService());
 			AppServiceProvider.Instance.Register(typeof(IExchangeRateService), new ExchangeRateService());
            AppServiceProvider.Instance.RegisterAsSingleton(typeof(IRegisteredCache), new RegisteredCache());
            AppServiceProvider.Instance.Register(typeof(IWealthService), new WealthService());
 			AppServiceProvider.Instance.Register(typeof(IAppUserRoleService), new AppUserRoleService());
 			//@RegisterBusinessPointer
        }

        public static void RegisterDataAccessServices()
        {
            
 			AppServiceProvider.Instance.Register(typeof(IAppUserDataAccess), new AppUserDataAccess());
 			AppServiceProvider.Instance.Register(typeof(IEmailTemplateDataAccess), new EmailTemplateDataAccess());
 			AppServiceProvider.Instance.Register(typeof(IExchangeRateDataAccess), new ExchangeRateDataAccess());
 			AppServiceProvider.Instance.Register(typeof(IWealthDataAccess), new WealthDataAccess());
 			AppServiceProvider.Instance.Register(typeof(IAppUserRoleDataAccess), new AppUserRoleDataAccess());
 			//@RegisterDataAccessPointer
        }

        public static void LoadParameterCache()
        {
            var cache = ExchangeRateCache.Instance;
            //var parameters = AppServiceProvider.Instance.Get<IAppParameterService>().GetAll();
            //if (parameters != null)
            //{
            //    parameters.ForEach(parameter => ParameterCache.Instance.TryAdd(parameter.ParameterCode, parameter.ParameterValue));
            //}
        }
    }
}
