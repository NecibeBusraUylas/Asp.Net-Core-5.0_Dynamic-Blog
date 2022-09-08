using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using DynamicBlog.Models;
using EntityLayer.Concrete;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dynamic_Blog
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
            services.AddControllersWithViews();

            //project level authorization
            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                            .RequireAuthenticatedUser()
                            .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });

            //authorization yokken sayfaya yönlendirmesini istediðimde hata vermesin login'e yönlendirsin
            services.AddMvc();
            services.AddAuthentication(
               CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie(x =>
               {
                   x.LoginPath = "/Login/Index";
               }
           );

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(100);

                options.AccessDeniedPath = new PathString("/Login/AccessDenied");
                options.LoginPath = "/Login/Index";
                options.SlidingExpiration = true;
            });

            services.AddDbContext<Context>();
            services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<Context>();
            // paroladaki zorunluluklardan bir kaçýndan kurtulmak istediðinde
            //services.AddIdentity<AppUser, AppRole>(x =>
            //{
            //    x.Password.RequireUppercase = false;
            //    x.Password.RequireNonAlphanumeric = false;
            //}).AddEntityFrameworkStores<Context>();

            services.AddSingleton(new WriterCity());

            services.AddValidatorsFromAssemblyContaining<BlogValidator>();

            //services.AddSession(); //session
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

            //error page
            //app.UseStatusCodePages();  yanlýþ istek olunca sayfada Status Code: 404 Not Found çýktýsý verir
            app.UseStatusCodePagesWithReExecute("/ErrorPage/Error404", "?code={0}"); //yanlýþ istekte Error Page içindeki Error1'e yönlendir hata code'u code içinde yazan

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            //app.UseSession(); //session

            app.UseRouting();

            app.UseAuthorization();

            app.UseAuthentication();

            //uygulama baþlatýlýnca ilk çalýþacak kýsým
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                    );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                    );
            });
        }
    }
}
