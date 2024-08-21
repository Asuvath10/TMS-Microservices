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
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using GlobalException;
using policy;
using System.Threading.Tasks;

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

            var key = Encoding.ASCII.GetBytes(Configuration["Jwt:Key"]);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddAuthorization();

            // Setting up services with httpclient
            services.AddHttpClient<IProposalLetterManagement, ProposalLetterManagement>(client =>
            {
                client.BaseAddress = new Uri(Configuration["ProposalService:BaseUrl"]);
                client.Timeout= TimeSpan.FromSeconds(30);
            })
            //Set lifetime to five minutes
            .SetHandlerLifetime(TimeSpan.FromMinutes(5))
            // Added retrypolicy for overcome socket exception.
            .AddHttpMessageHandler(() => new PolicyHttpMessageHandler(RetryPolicy.GetRetryPolicy()));

            // Setting up services with httpclient
            services.AddHttpClient<IUserManagement, UserManagement>(client =>
            {
                client.BaseAddress = new Uri(Configuration["UserService:BaseUrl"]);
                client.Timeout= TimeSpan.FromSeconds(30);
            })
            //Set lifetime to five minutes
            .SetHandlerLifetime(TimeSpan.FromMinutes(5))
            // Added retrypolicy for overcome socket exception.
            .AddHttpMessageHandler(() => new PolicyHttpMessageHandler(RetryPolicy.GetRetryPolicy()));

            // Setting up document services with httpclient
            services.AddHttpClient<IDocumentManagement, DocumentManagement>(client =>
            {
                client.BaseAddress = new Uri(Configuration["DocumentService:BaseUrl"]);
                client.Timeout= TimeSpan.FromSeconds(30);

            })
            //Set lifetime to five minutes
            .SetHandlerLifetime(TimeSpan.FromMinutes(5))
            // Added retrypolicy for overcome socket exception.
            .AddHttpMessageHandler(() => new PolicyHttpMessageHandler(RetryPolicy.GetRetryPolicy()));

            // Adding Ocelot service
            services.AddOcelot();

            //services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                c.IgnoreObsoleteActions();
                c.IgnoreObsoleteProperties();
                c.CustomSchemaIds(type => type.FullName);
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "APIGateway", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                 {
                        new OpenApiSecurityScheme {
                        Reference = new OpenApiReference {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                        }
                    },
                     new string[] {}
                 }
                });
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

            app.UseMiddleware<GlobalExceptionMiddleware>();

            app.UseCors("default");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            await app.UseOcelot();
        }
    }
}
