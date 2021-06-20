using AutoMapper;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using PixelStamp.Core.Entities;
using PixelStamp.Infrastructure.DbContexts;
using PixelStamp.ServiceInterface.Auth;
using System;
using System.Text;

namespace PixelStamp.Portal
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
            // Access the HttpContext inside a service.
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // very important to includes input and output formatters for JSON (such as Enum)
            // Install - Package Microsoft.AspNetCore.Mvc.NewtonsoftJson - Version 3.1.3
            services.AddControllersWithViews();

            // AddDbContext ==> Install-Package Microsoft.AspNetCore.Identity.EntityFrameworkCore -Version 3.1.3
            // UseSqlServer ==> Install - Package Microsoft.EntityFrameworkCore.SqlServer - Version 3.1.3
            services.AddDbContext<CommandDbContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionStrings:Default"],
                    sqlOption =>
                    {
                        //sqlOption.EnableRetryOnFailure();
                        sqlOption.MigrationsAssembly("PixelStamp.Infrastructure");
                    });
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromDays(30);
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options =>
                    {
                        options.LoginPath = "/Account/login/";

                    }); services.ConfigureApplicationCookie(options =>
                    {
                        options.ExpireTimeSpan = TimeSpan.FromDays(30);
                    });
            services.AddSession();
            // adds the default identity system configuration
            var builder = services.AddIdentity<AppUser, AppRole>(options =>
            {
                // configure identity options
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
            });

            builder.AddEntityFrameworkStores<CommandDbContext>().AddDefaultTokenProviders();

            //  Add AutoMapper ==> 
            //  Install - Package AutoMapper - Version 10.0.0
            //  Install - Package AutoMapper.Extensions.Microsoft.DependencyInjection - Version 8.0.1
            services.AddAutoMapper();

            // Add Hangfire services.
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(Configuration.GetConnectionString("Default"), new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                }));

            // Add ASP.Net Core Session State.
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(60); 
                options.Cookie = new CookieBuilder() { Name = "PixelStamp.Portal.LocalAuth", HttpOnly = true };
            });

            // Add the processing server as IHostedService
            services.AddHangfireServer();

            // Configure repositories.
            services.AddRepositories();

            // Add application services.
            services.AddServices();


            // Add application repositories.
            services.AddRepositories();

            JwtIssuerOptions(services);
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();
            app.Use(async (context, next) =>
            {
                var Token = context.Session.GetString("token");
                if (!string.IsNullOrEmpty(Token))
                {
                    context.Request.Headers.Add("Authorization", Token);
                }
                await next();
            });

            app.UseAuthentication();

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapHangfireDashboard();
            });
        }

        private void JwtIssuerOptions(IServiceCollection services)
        {
            services.AddScoped<IJwtFactory, JwtFactory>();


            // jwt wire up
            // Get options from app settings
            var JwtIssuerOptions = Configuration.GetSection("JwtIssuerOptions");

            // key generated using symmetric algorithms
            var _signingKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(JwtIssuerOptions.GetValue<string>("Secret")));

            // Configure JwtIssuerOptions
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = JwtIssuerOptions.GetValue<string>("Issuer");
                options.Audience = JwtIssuerOptions.GetValue<string>("Audience");
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });

            // Add Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = JwtIssuerOptions.GetValue<string>("Issuer");
                configureOptions.SaveToken = true;
                configureOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    IssuerSigningKey = _signingKey,
                    ValidIssuer = JwtIssuerOptions.GetValue<string>("Issuer"),
                    ValidateAudience = true,
                    ValidAudience = JwtIssuerOptions.GetValue<string>("Audience"),
                    ValidateIssuerSigningKey = true,
                    RequireExpirationTime = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

        }




    }
}
