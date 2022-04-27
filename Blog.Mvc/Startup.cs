using Blog.Mvc.AutoMapper.Profiles;
using Blog.Mvc.Helpers.Abstract;
using Blog.Mvc.Helpers.Concrete;
using Blog.Services.AutoMapper.Profiles;
using Blog.Services.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Text.Json.Serialization;

namespace Blog.Mvc
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; } // Okuma g�revi g�r�r
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(options =>
            {
                options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(value => "Bu alan bo� ge�ilmemelidir."); // Di�er alanlar i�in de bu alan ge�erlidir.
            }).AddRazorRuntimeCompilation().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            }).AddNToastNotifyToastr();
            services.AddSession();
            services.AddAutoMapper(typeof(ArticleProfile), typeof(CategoryProfile), typeof(UserProfile), typeof(ViewModelsProfile), typeof(CommentProfile));
            services.LoadMyServices(connectionString: Configuration.GetConnectionString("LocalDB"));
            services.AddScoped<IImageHelper, ImageHelper>();
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/Admin/Auth/Login");
                options.LogoutPath = new PathString("/Admin/Auth/Logout");
                options.AccessDeniedPath = new PathString("/Admin/Auth/AccessDenied"); // Sistem i�erisinde giri� yapm�� ama yetkisi olmayan bir yere ula��rsa bu sayfaya y�nlendirilir
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
            app.UseNToastNotify();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    name: "Admin", areaName: "Admin", pattern: "Admin/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
