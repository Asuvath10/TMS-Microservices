using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using TMS.Models;
using Microsoft.EntityFrameworkCore;
using PLManagement.Services;
using PLManagement.Repositories;
using PLManagement.Interfaces.services;
using PLManagement.Interfaces.Repos;
using System;
using Google.Cloud.Storage.V1;
using GemBox.Document;
using PLManagement.Models;
using GlobalException;
using PLManagement.Interfaces;
using Microsoft.Extensions.Http;
using policy;

namespace PLManagement
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
            services.AddHttpClient();
            services.AddTransient<IPLService, PLService>();
            services.AddTransient<IFormService, FormService>();
            services.AddTransient<IPLStatusService, PLStatusService>();
            services.AddTransient<IPLRepository, PLRepository>();
            services.AddTransient<IFormRepository, FormRepository>();
            services.AddTransient<IPLStatusRepository, PLStatusRepository>();
            //Added dbcontext
            services.AddDbContext<PLManagementContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PLManagement", Version = "v1" });
            });
            services.AddCors((setup) =>
            {
                setup.AddPolicy("default", (options) =>
                {
                    options.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PLManagement v1"));
            }
            app.UseMiddleware<GlobalExceptionMiddleware>();
            app.UseCors("default");
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
