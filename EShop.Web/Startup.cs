using EShop.Web.Code;
using EShop.Web.Data;
using EShop.Web.Models;
using EShop.Web.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EShop.Web
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;


            });
            services.AddDistributedMemoryCache();

            services.AddSession(opt =>
            {
                opt.IdleTimeout = TimeSpan.FromDays(10);
                opt.Cookie.HttpOnly = true;
                opt.Cookie.IsEssential = true;
            });
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
                    {
                        options.Password.RequireUppercase = false;
                        options.Password.RequireNonAlphanumeric = false;
                        options.Password.RequireLowercase = false;
                        options.Password.RequiredLength = 5;
                        options.Password.RequireDigit = false;
                        options.Password.RequiredUniqueChars = 3;
                    })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                //.AddDefaultUI(UIFramework.Bootstrap4)
                .AddDefaultTokenProviders();
            //services.AddDefaultIdentity<IdentityUser>()
            //    .AddDefaultUI(UIFramework.Bootstrap4)
            //    .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            //services.AddSession(options =>
            //{
            //    options.Cookie.Name = ".EShop.Session";
            //    // Set a short timeout for easy testing.
            //    options.IdleTimeout = TimeSpan.FromDays(20);
            //    options.Cookie.HttpOnly = true;
            //});
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            //services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ICart, Cart>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
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

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            app.UseAuthentication();
            app.UseSession();
            IdentityDbInitializer.SeedData(userManager, roleManager);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Product}/{action=Index}/{id?}");
            });
        }
    }
}
