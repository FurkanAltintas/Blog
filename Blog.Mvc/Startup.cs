using Blog.Mvc.AutoMapper.Profiles;
using Blog.Mvc.Helpers.Abstract;
using Blog.Mvc.Helpers.Concrete;
using Blog.Services.AutoMapper.Profiles;
using Blog.Services.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Text.Json.Serialization;

namespace Blog.Mvc
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddRazorRuntimeCompilation().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            });
            services.AddSession();
            services.AddAutoMapper(typeof(ArticleProfile), typeof(CategoryProfile), typeof(UserProfile));
            services.LoadMyServices();
            services.AddScoped<IImageHelper, ImageHelper>();
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/Admin/User/Login");
                options.LogoutPath = new PathString("/Admin/User/Logout");
                options.AccessDeniedPath = new PathString("/Admin/User/AccessDenied"); // Sistem i�erisinde giri� yapm�� ama yetkisi olmayan bir yere ula��rsa bu sayfaya y�nlendirilir
                options.Cookie = new CookieBuilder
                {
                    Name = "Blog",
                    HttpOnly = true, // Sadece http �zerinden g�ndermemizi sa�l�yor
                    SameSite = SameSiteMode.Strict, // Bu cookie bilgilerini sadece kendi sitemizden geldi�inde kabul edecek
                    SecurePolicy = CookieSecurePolicy.SameAsRequest // �stek bizi http �zerinden gelirse http, https �zerinden gelirse https olur. (Bunun ger�ek uygulamalarda kullan�m� Always olur. (Sadece https �zerinden olur.))
                };
                options.SlidingExpiration = true; // Kullan�c� tekrardan sisteme giri� yaparsa oturum s�resini tekrardan uzat�r
                options.ExpireTimeSpan = TimeSpan.FromDays(7); // 7 g�n boyunca kullan�c�n�n tekrardan giri� yapmas�na gerek kalm�yacak.
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }
            app.UseSession();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication(); // Kimlik kontrol�
            app.UseAuthorization(); // Yetki kontrol�
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    name: "Admin", areaName: "Admin", pattern: "Admin/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
