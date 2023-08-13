using Demo.API.Infrastructure;
using Demo.BLL.Interfaces;
using Demo.BLL.Services;
using Demo.DAL.Interfaces;
using Demo.DAL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;
using Tools.Connection;

namespace Demo.API
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
            services.AddCors(o => o.AddPolicy("MyPolicy", builder => {
                builder.WithOrigins("http://localhost:4200")
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials();
            }));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true, // Vérifie automatiquement l'expiration du token
                    ValidIssuer = this.Configuration["Jwt:myIssuer"],
                    ValidAudience = this.Configuration["Jwt:myAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.Configuration["Jwt:Key"])),
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddControllers();

            services.AddSignalR();
            services.AddSingleton<TokenManager>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Make3D.API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n" +
                                "Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\n" +
                                "Example: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"

                });

                OpenApiSecurityScheme openApiSecurityScheme = new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header
                };

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    //Défini une paire de clé valeur au niveau du dictionnaire
                    [openApiSecurityScheme] = new List<string>()
                });

            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("IsConnected", policy => policy.RequireAuthenticatedUser());
            });


            // Singletons
            services.AddSingleton(sp => new Connection(Configuration.GetConnectionString("default")));

            // Repositories
            services.AddScoped<IUtilisateurRepository, UtilisateurRepository>();

            // Services
            services.AddScoped<IUtilisateurService, UtilisateurService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // ajout cors permettant n'importe quel site d'accéder à l'API
            app.UseCors("MyPolicy");

            /*
            // ajout d'une origine en indiquant son nom de domaine
            app.UseCors(options =>
            {
                options.WithOrigins("http://http://localhost:4200").AllowAnyMethod();
                // options.WithOrigins("mon autre site...").AllowAnyMethod();
            });
            */

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Make3D.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
