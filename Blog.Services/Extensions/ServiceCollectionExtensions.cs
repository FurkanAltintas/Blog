using Blog.Data.Abstract;
using Blog.Data.Concrete;
using Blog.Data.Concrete.EntityFramework.Contexts;
using Blog.Entities.Concrete;
using Blog.Services.Abstract;
using Blog.Services.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Blog.Services.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection LoadMyServices(this IServiceCollection serviceCollection, string connectionString)
        {
            serviceCollection.AddDbContext<BlogContext>(options => options.UseSqlServer(connectionString));

            serviceCollection.AddIdentity<User, Role>(options =>
            {
                // User Password Options
                options.Password.RequireDigit = false; // Rakam bulunsun mu ?
                options.Password.RequiredLength = 6; // Zorunlu uzunluk karakteri (Kaç karakter olsun ?) // Minimum 10 veya 12 vermemiz daha iyi olur güvenlik için
                options.Password.RequiredUniqueChars = 0; // Özel karakterlerden kaç tane olsun ? Unique karakterlerden kaç tane olması gerekiyor. (Özel karakterler !, ?)
                options.Password.RequireNonAlphanumeric = false; // Özel karakterler kullanılsın mı ? Özel karakterlerin kullanılmasını sağlıyor. (@, !, ?, $)
                options.Password.RequireLowercase = false; // Küçük harfler zorunlu olsun mu ?
                options.Password.RequireUppercase = false; // Büyük harfler zorunlu olsun mu ?
                // User UserName and Email Options
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+$"; // Kullanıcı adı oluşturulurken verilen karakterler (Karakterleri yan yana yazmamız gerekmektedir.)
                options.User.RequireUniqueEmail = true; // Bir kullanıcı kayıt olurken bu email adresinden sadece bir kayıt mı bulunsun ?
            }).AddEntityFrameworkStores<BlogContext>();

            serviceCollection.Configure<SecurityStampValidatorOptions>(options =>
            {
                options.ValidationInterval = TimeSpan.FromMinutes(15);  // Kontrol süresini ayarlamamızı sağlıyor. Bu kontrol yapıldığında veritabanına bir istekte bulunulunuyor.
                // Bir kullanıcıda rol ataması yapıldıktan sonra 15 dakika içerisinde logout olucak ve giriş yapması gerekicek.
            });

            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
            serviceCollection.AddScoped<ICategoryService, CategoryManager>();
            serviceCollection.AddScoped<ICommentService, CommentManager>();
            serviceCollection.AddScoped<IArticleService, ArticleManager>();
            serviceCollection.AddSingleton<IMailService, MailManager>();

            return serviceCollection;
        }
    }
}