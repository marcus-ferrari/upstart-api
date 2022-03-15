using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Upstart.Domain.Interfaces.ExternalServices;
using Upstart.Domain.Interfaces.Services;
using Upstart.Domain.Services;
using Upstart.Infrastructure.ExternalServices;
using Upstart.Infrastructure.Mapping;

namespace Upstart.Application
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Upstart.Application", Version = "v1" });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            services.AddScoped<IGeoLocationService, GeoCensusService>();
            services.AddScoped<IExternalForecastService, WeatherGovForecastService>();
            services.AddScoped<IForecastService, ForecastService>();

            services.AddFluentValidation();

            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<ForecastPeriodProfile>();
            });

            services.AddCors(options =>
             {
                 options.AddPolicy("Localhost",
                     builder =>
                     {
                         builder.WithOrigins("http://localhost:3000/")
                                             .AllowAnyHeader()
                                             .AllowAnyMethod()
                                             .AllowAnyOrigin();
                     });
             });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Upstart.Application v1"));
            }
            app.UseExceptionHandler("/error");  

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
