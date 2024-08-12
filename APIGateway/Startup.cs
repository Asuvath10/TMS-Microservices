using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Net.Http;
using APIGateway.Interfaces;
using APIGateway.Services;
using Polly;
using Microsoft.Extensions.Http;
using Polly.Extensions.Http;

namespace APIGateway
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
            // Setting up services with httpclient
            services.AddHttpClient<IProposalLetterManagement, ProposalLetterManagement>(client =>
            {
                client.BaseAddress = new Uri(Configuration["ProposalService:BaseUrl"]);
            })
            .SetHandlerLifetime(TimeSpan.FromMinutes(5))  //Set lifetime to five minutes
            // Added retrypolicy for overcome socket exception.
            .AddHttpMessageHandler(() => new PolicyHttpMessageHandler(GetRetryPolicy()));

            // Setting up services with httpclient
            services.AddHttpClient<IUserManagement, UserManagement>(client =>
            {
                client.BaseAddress = new Uri(Configuration["UserService:BaseUrl"]);
            })
            .SetHandlerLifetime(TimeSpan.FromMinutes(5))  //Set lifetime to five minutes
            // Added retrypolicy for overcome socket exception.
            .AddHttpMessageHandler(() => new PolicyHttpMessageHandler(GetRetryPolicy()));

            // Adding Ocelot service
            services.AddOcelot();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "APIGateway", Version = "v1" });
            });

            //Setting CORS
            services.AddCors((setup) =>
            {
                setup.AddPolicy("default", (options) =>
                {
                    options.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "APIGateway v1"));
            }

            app.UseCors("default");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            await app.UseOcelot();
        }

        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2,
                                                                            retryAttempt)));
        }
    }
}
