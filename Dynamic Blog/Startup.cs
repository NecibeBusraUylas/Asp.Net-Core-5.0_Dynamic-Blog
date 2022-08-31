using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using DynamicBlog.Models;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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

            //authorization yokken sayfaya y�nlendirmesini istedi�imde hata vermesin login'e y�nlendirsin
            services.AddMvc();
            services.AddAuthentication(
               CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie(x =>
               {
                   x.LoginPath = "/Login/Index";
               }
           );

            services.AddSingleton<IAboutService>(new AboutManager(new EFAboutRepository()));

            services.AddSingleton<IBlogService>(new BlogManager(new EFBlogRepository()));

            services.AddSingleton<ICategoryService>(new CategoryManager(new EFCategoryRepository()));

            services.AddSingleton<ICommentService>(new CommentManager(new EFCommentRepository()));

            services.AddSingleton<IContactService>(new ContactManager(new EFContactRepository()));

            services.AddSingleton<IMessage2Service>(new Message2Manager(new EFMessage2Repository()));

            services.AddSingleton<INewsLetterService>(new NewsLetterManager(new EFNewsLetterRepository()));

            services.AddSingleton<INotificationService>(new NotificationManager(new EFNotificationRepository()));

            services.AddSingleton<IWriterService>(new WriterManager(new EFWriterRepository()));

            services.AddSingleton<IAdminService>(new AdminManager(new EFAdminRepository()));

            services.AddSingleton(new GetUserInfo());

            services.AddControllersWithViews().AddFluentValidation(x =>
            x.RegisterValidatorsFromAssemblyContaining<BlogValidator>());

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
            //app.UseStatusCodePages();  yanl�� istek olunca sayfada Status Code: 404 Not Found ��kt�s� verir
            app.UseStatusCodePagesWithReExecute("/ErrorPage/Error404", "?code={0}"); //yanl�� istekte Error Page i�indeki Error1'e y�nlendir hata code'u code i�inde yazan

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            //app.UseSession(); //session

            app.UseRouting();

            app.UseAuthorization();

            app.UseAuthentication();

            //uygulama ba�lat�l�nca ilk �al��acak k�s�m
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
