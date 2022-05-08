using AutoMapper;
using Blog.Entities.Concrete;
using Blog.Mvc.Areas.Admin.Models;
using Blog.Services.Abstract;
using Blog.Shared.Utilities.Helpers.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NToastNotify;
using System.Threading.Tasks;

namespace Blog.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class OptionsController : Controller
    {
        private readonly AboutUsPageInfo _aboutUsPageInfo;
        private readonly IWritableOptions<AboutUsPageInfo> _aboutUsPageInfoWrite;
        private readonly IToastNotification _toastNotification;
        private readonly WebsiteInfo _websiteInfo;
        private readonly IWritableOptions<WebsiteInfo> _websiteInfoWriter;
        private readonly SmtpSettings _smtpSettings;
        private readonly IWritableOptions<SmtpSettings> _smtpSettingsWriter;
        private readonly ArticleRightSideBarWidgetOptions _articleRightSideBarWidgetOptions;
        private readonly IWritableOptions<ArticleRightSideBarWidgetOptions> _articleRightSideBarWidgetOptionsWriter;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public OptionsController(IOptionsSnapshot<AboutUsPageInfo> aboutUsPageInfo, IWritableOptions<AboutUsPageInfo> aboutUsPageInfoWrite, IToastNotification toastNotification, IOptionsSnapshot<WebsiteInfo> websiteInfo, IWritableOptions<WebsiteInfo> websiteInfoWriter, IOptionsSnapshot<SmtpSettings> smtpSettings, IWritableOptions<SmtpSettings> smtpSettingsWriter, IOptions<ArticleRightSideBarWidgetOptions> articleRightSideBarWidgetOptions, IWritableOptions<ArticleRightSideBarWidgetOptions> articleRightSideBarWidgetOptionsWriter, ICategoryService categoryService, IMapper mapper)
        {
            _aboutUsPageInfo = aboutUsPageInfo.Value;
            _aboutUsPageInfoWrite = aboutUsPageInfoWrite;
            _toastNotification = toastNotification;
            _websiteInfo = websiteInfo.Value;
            _websiteInfoWriter = websiteInfoWriter;
            _smtpSettings = smtpSettings.Value;
            _smtpSettingsWriter = smtpSettingsWriter;
            _articleRightSideBarWidgetOptions = articleRightSideBarWidgetOptions.Value;
            _articleRightSideBarWidgetOptionsWriter = articleRightSideBarWidgetOptionsWriter;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult About()
        {
            return View(_aboutUsPageInfo);
        }

        [HttpPost]
        public IActionResult About(AboutUsPageInfo aboutUsPageInfo)
        {
            if (ModelState.IsValid)
            {
                _aboutUsPageInfoWrite.Update(a =>
                {
                    a.Header = aboutUsPageInfo.Header;
                    a.Content = aboutUsPageInfo.Content;
                    a.SeoAuthor = aboutUsPageInfo.SeoAuthor;
                    a.SeoDescription = aboutUsPageInfo.SeoDescription;
                    a.SeoTags = aboutUsPageInfo.SeoTags;
                });
                _toastNotification.AddSuccessToastMessage("Hakkımızda Sayfa İçerikleri Başarıyla Güncellenmiştir", new ToastrOptions
                {
                    Title = "Başarılı İşlem!"
                });
                return View();
            }
            return View(aboutUsPageInfo);
        }

        [HttpGet]
        public IActionResult GeneralSettings()
        {
            return View(_websiteInfo);
        }

        [HttpPost]
        public IActionResult GeneralSettings(WebsiteInfo websiteInfo)
        {
            if (ModelState.IsValid)
            {
                _websiteInfoWriter.Update(w =>
                {
                    w.Title = websiteInfo.Title;
                    w.MenuTitle = websiteInfo.MenuTitle;
                    w.SeoAuthor = websiteInfo.SeoAuthor;
                    w.SeoDescription = websiteInfo.SeoDescription;
                    w.SeoTags = websiteInfo.SeoTags;
                });
                _toastNotification.AddSuccessToastMessage("Sitenizin Genel Ayarları Başarıyla Güncellenmiştir", new ToastrOptions
                {
                    Title = "Başarılı İşlem!"
                });
                return View();
            }
            return View(websiteInfo);
        }

        [HttpGet]
        public IActionResult EmailSettings()
        {
            return View(_smtpSettings);
        }

        [HttpPost]
        public IActionResult EmailSettings(SmtpSettings smtpSettings)
        {
            if (ModelState.IsValid)
            {
                _smtpSettingsWriter.Update(s =>
                {
                    s.Server = smtpSettings.Server;
                    s.Port = smtpSettings.Port;
                    s.SenderName = smtpSettings.SenderName;
                    s.SenderEmail = smtpSettings.SenderEmail;
                    s.Username = smtpSettings.Password;
                    s.Password = smtpSettings.Password;
                });
                _toastNotification.AddSuccessToastMessage("Sitenizin E-Posta Ayarları Başarıyla Güncellenmiştir", new ToastrOptions
                {
                    Title = "Başarılı İşlem!"
                });
                return View();
            }
            return View(smtpSettings);
        }

        [HttpGet]
        public async Task<IActionResult> ArticleRightSideBarWidgetSettings()
        {
            var categoriesResult = await _categoryService.GetAllByNonDeletedAndActiveAsync();
            var articleRightSideBarWidgetOptionsViewModel = _mapper.Map<ArticleRightSideBarWidgetOptionsViewModel>(_articleRightSideBarWidgetOptions);
            articleRightSideBarWidgetOptionsViewModel.Categories = categoriesResult.Data.Categories;
            return View(articleRightSideBarWidgetOptionsViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ArticleRightSideBarWidgetSettings(ArticleRightSideBarWidgetOptionsViewModel articleRightSideBarWidgetOptionsViewModel)
        {
            if (ModelState.IsValid)
            {
                _articleRightSideBarWidgetOptionsWriter.Update(a =>
                {
                    a.Header = articleRightSideBarWidgetOptionsViewModel.Header;
                    a.TakeSize = articleRightSideBarWidgetOptionsViewModel.TakeSize;
                    a.CategoryId = articleRightSideBarWidgetOptionsViewModel.CategoryId;
                    a.FilterBy = articleRightSideBarWidgetOptionsViewModel.FilterBy;
                    a.OrderBy = articleRightSideBarWidgetOptionsViewModel.OrderBy;
                    a.IsAscending = articleRightSideBarWidgetOptionsViewModel.IsAscending;
                    a.StartAt = articleRightSideBarWidgetOptionsViewModel.StartAt;
                    a.EndAt = articleRightSideBarWidgetOptionsViewModel.EndAt;
                    a.MaxViewCount = articleRightSideBarWidgetOptionsViewModel.MaxViewCount;
                    a.MinViewCount = articleRightSideBarWidgetOptionsViewModel.MinViewCount;
                    a.MaxCommentCount = articleRightSideBarWidgetOptionsViewModel.MaxCommentCount;
                    a.MinCommentCount = articleRightSideBarWidgetOptionsViewModel.MinCommentCount;
                });
                _toastNotification.AddSuccessToastMessage("Makale Sayfalarınızın Widget Ayarları Başarıyla Güncellenmiştir", new ToastrOptions
                {
                    Title = "Başarılı İşlem!"
                });
            }

            var categoriesResult = await _categoryService.GetAllByNonDeletedAndActiveAsync();
            articleRightSideBarWidgetOptionsViewModel.Categories = categoriesResult.Data.Categories;
            return View(articleRightSideBarWidgetOptionsViewModel);
        }
    }
}