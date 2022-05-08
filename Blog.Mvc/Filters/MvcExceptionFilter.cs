using Blog.Shared.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Data.SqlTypes;

namespace Blog.Mvc.Filters
{
    public class MvcExceptionFilter : IExceptionFilter
    {
        private readonly IHostEnvironment _environment;
        private readonly IModelMetadataProvider _metadataProvider;
        private readonly ILogger _logger;

        public MvcExceptionFilter(IHostEnvironment environment, IModelMetadataProvider metadataProvider, ILogger<MvcExceptionFilter> logger)
        {
            _environment = environment;
            _metadataProvider = metadataProvider;
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            if (_environment.IsDevelopment()) // Hangi ortamda olunduğu kontrol edildi
            {
                context.ExceptionHandled = true; // Hata bizler tarafından ele alındığı için true yapıyoruz.
                MvcErrorModel mvcErrorModel = new();

                switch (context.Exception)
                {
                    case SqlNullValueException:
                        mvcErrorModel.Message = "Üzgünüz, işleminiz sırasında beklenmedik bir veritabanı hatası oluştu. Sorunu en kısa sürece çözeceğiz.";
                        mvcErrorModel.Detail = context.Exception.Message;
                        _logger.LogError(context.Exception, context.Exception.Message);
                        break;

                    case NullReferenceException:
                        mvcErrorModel.Message = "Üzgünüz, işleminiz sırasında beklenmedik bir null veriye rastlandı. Sorunu en kısa sürece çözeceğiz.";
                        mvcErrorModel.Detail = context.Exception.Message;
                        _logger.LogError(context.Exception, context.Exception.Message);
                        break;

                    default:
                        mvcErrorModel.Message = "Üzgünüz, işleminiz sırasında beklenmedik bir hata oluştu. Sorunu en kısa sürece çözeceğiz.";
                        _logger.LogError(context.Exception, "Bu benim log hata mesajım!");
                        break;
                }

                ViewResult result = new() { ViewName = "Error" }; // Hata sayfasının ismi
                result.StatusCode = 500; // StatusCode 500 olarak ayarlandı
                result.ViewData = new(_metadataProvider, context.ModelState); // new ViewDataDictionary
                result.ViewData.Add("MvcErrorModel", mvcErrorModel); // Modelimize isim verdik kullanabilmek için
                context.Result = result; // Kullanıcıya dönücek resultı atamış olduk
            }
        }
    }
}
