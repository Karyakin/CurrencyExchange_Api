using AutoMapper;
using Contracts;
using LoggerService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;
using Services;
using Services.Helpers;
using System;
using System.IO;
using СurrencyExchange.Extensions;
using СurrencyExchange.Filters;
using СurrencyExchange.Helpers;

namespace СurrencyExchange
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        //This method gets called by the runtime. Use this method to add services to the container.
        //Если бы сервисов было много, я бы создал отдельный класс Extensions, туда бы полжил в методы сгруппированые по смыслу сервисы и из 
        //Startup обращался бы к этому классу. Функционал один и тот же, но разгружает Startup
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddScoped<FilterAttribute>();
            services.AddSingleton<ILoggerManager, LoggerManager>();
            services.AddHttpClient<IExchangeService, ExchangeService>(client =>
            {
                client.BaseAddress = new Uri(Configuration.GetSection("NationalBankSettings")["BaseUrl"]);
            });

            services.Configure<NationalBankSettings>(Configuration.GetSection("NationalBankSettings"));
            services.AddAutoMapper(typeof(AutomapperProfile).Assembly);
            services.AddScoped<FilterAttribute>();
            services.ConfigureLoggerService();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
