using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using PixelStamp.Infrastructure.DbContexts;
using PixelStamp.Core.Entities;
using PixelStamp.Core.Interfaces.Services;
using PixelStamp.ServiceInterface;

namespace ISolutionTask
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddDbContext<CommandDbContext>(options =>
               options.UseSqlServer(
                   Configuration.GetConnectionString("PixelStamp")));



            services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<CommandDbContext>();



            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IUserService, UserService>();

            services.AddAutoMapper();

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //// Registe our services with Autofac container
            //ContainerBuilder builder = new ContainerBuilder();
            //builder.RegisterModule(new AutoFacConfiguration());
            //builder.Populate(services);
            //IContainer container = builder.Build();
            ////Create the IServiceProvider based on the container.
            //return new AutofacServiceProvider(container);

            //Now register our services with Autofac container

            var builder = new ContainerBuilder();

            builder.RegisterModule(new AutoFacConfiguration());

            builder.Populate(services);

            var container = builder.Build();

            // Create the IServiceProvider based on the container.

            return new AutofacServiceProvider(container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider, UserDbContext userDbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            DataSeedingIntilization.Seed(userDbContext, serviceProvider);


            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
