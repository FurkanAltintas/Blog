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
                options.AccessDeniedPath = new PathString("/Admin/User/AccessDenied"); // Sistem içerisinde giriþ yapmýþ ama yetkisi olmayan bir yere ulaþýrsa bu sayfaya yönlendirilir
                options.Cookie = new CookieBuilder
                {
                    Name = "Blog",
                    HttpOnly = true, // Sadece http üzerinden göndermemizi saðlýyor
                    SameSite = SameSiteMode.Strict, // Bu cookie bilgilerini sadece kendi sitemizden geldiðinde kabul edecek
                    SecurePolicy = CookieSecurePolicy.SameAsRequest // Ýstek bizi http üzerinden gelirse http, https üzerinden gelirse https olur. (Bunun gerçek uygulamalarda kullanýmý Always olur. (Sadece https üzerinden olur.))
                };
                options.SlidingExpiration = true; // Kullanýcý tekrardan sisteme giriþ yaparsa oturum süresini tekrardan uzatýr
                options.ExpireTimeSpan = TimeSpan.FromDays(7); // 7 gün boyunca kullanýcýnýn tekrardan giriþ yapmasýna gerek kalmýyacak.
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
            app.UseAuthentication(); // Kimlik kontrolü
            app.UseAuthorization(); // Yetki kontrolü
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    name: "Admin", areaName: "Admin", pattern: "Admin/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
