using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using PPCT.DataSupport;
using PPCT.RepositoryServices.Helpers.HistoryRecordCreators;
using PPCT.RepositoryServices.JWTRepoServices;
using PPCT.RepositoryServices.RetailStoreRepoServices;
using PPCT.RepositoryServices.VATRepoServices;
using System.Text;

namespace PPCT.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            #region Dependency Injection
            //Mention which type of instance want to inject -(the lifetime)
            //Ex. 1)services.AddSingleton 2)services.AddTransient 3)services.AddScoped
            //MORE: https://henriquesd.medium.com/dependency-injection-and-service-lifetimes-in-net-core-ab9189349420

            services.AddScoped<IRetailStoreRepo, RetailStoreRepo>();
            services.AddScoped<IRetailStoreServices, RetailStoreServices>();

            services.AddScoped<IVATRepo, VATRepo>();
            services.AddScoped<IVATService, VATService>();
            services.AddScoped<RetailStoreVATPercentageHistoryRecorder>();

            services.AddScoped<IJwtAuthTokenGenerator, JwtAuthTokenGenerator>();
            #endregion

            #region Enable CORS
            //services.AddCors();
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });
            #endregion

            #region JSON Serializer
            //services.AddControllers();
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore).AddNewtonsoftJson(options =>
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver());
            #endregion

            #region Enebale Db Connection
            services.AddDbContext<DatabaseContext>(options =>  {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            #endregion

            #region For Identity  
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<DatabaseContext>()
                .AddDefaultTokenProviders();
            #endregion

            #region Adding Authentication & Jwt Bearer
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
             {
                 options.SaveToken = true;
                 options.RequireHttpsMetadata = false;
                 options.TokenValidationParameters = new TokenValidationParameters()
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     ClockSkew = System.TimeSpan.Zero,

                     ValidAudience = Configuration["JWT:ValidAudience"],
                     ValidIssuer = Configuration["JWT:ValidIssuer"],
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                 };
             });
            #endregion

            #region Enable Swagger
            services.AddSwaggerGen(c =>
            {
                //This is to generate the Default UI of Swagger Documentation
                c.SwaggerDoc("v1", new OpenApiInfo 
                { 
                    Title = "PPCT.WebAPI", 
                    Version = "v1" 
                });

                // To Enable authorization using Swagger (JWT) 
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
                    }
                });
            });
            #endregion
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //.AllowAnyOrigin()
            //.AllowAnyMethod()
            //.AllowAnyHeader()
            app.UseCors(options => options
                //.WithOrigins(new []{ "http://localhost:3000/", "http://localhost:8080/"})
                .AllowAnyHeader()
                .AllowAnyMethod()
                //.AllowCredentials()
                .AllowAnyOrigin()
            );
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PPCT.WebAPI v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}