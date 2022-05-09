using Blog.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Mvc.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)] // Bu filtreyi class veya methodlarda kullanabileceğimizi belirtiyoruz
    public class ViewCountFilterAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var articleId = context.ActionArguments["articleId"];
            if (articleId is not null) // articleId null değil ise
            {
                string articleValue = context.HttpContext.Request.Cookies[$"article{articleId}"]; // article1, article13
                if (string.IsNullOrEmpty(articleValue)) // değer Null veya boş ise daha önce kullanıcı bu makaleyi okumamış demektir.
                {
                    Set(key: $"article{articleId}", value: articleId.ToString(), expireTime: 1, response: context.HttpContext.Response);
                    var articleService = context.HttpContext.RequestServices.GetService<IArticleService>(); // using Microsoft.Extensions.DependencyInjection;
                    await articleService.IncreaseViewCountAsync(Convert.ToInt32(articleId));
                    await next(); // View yüklenmesini sağladık
                }
            }
            await next();
        }

        /// <summary>  
        /// set the cookie  
        /// </summary>  
        /// <param name="key">key (unique indentifier)</param>  
        /// <param name="value">value to store in cookie object</param>  
        /// <param name="expireTime">expiration time</param>  
        public void Set(string key, string value, int? expireTime, HttpResponse response)
        {
            CookieOptions option = new();

            if (expireTime.HasValue)
                option.Expires = DateTime.Now.AddYears(expireTime.Value);
            else
                option.Expires = DateTime.Now.AddMonths(6); // herhangi bir değer gönderilmez ise 6 ay olarak ayarlanıyor

            response.Cookies.Append(key, value, option);
        }

        /// <summary>  
        /// Delete the key  
        /// </summary>  
        /// <param name="key">Key</param>  
        public void Remove(string key, HttpResponse response)
        {
            response.Cookies.Delete(key);
        }
    }
}