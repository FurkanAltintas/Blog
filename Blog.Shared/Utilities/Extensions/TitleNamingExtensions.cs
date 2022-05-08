namespace Blog.Shared.Utilities.Extensions
{
    public static class TitleNamingExtensions
    {
        public static string TitleNameByControllerName(this string controller)
        {
            switch (controller)
            {
                case "Home":
                    return "Yönetim Paneli";
                case "Article":
                    return "Makaleler";
                case "Category":
                    return "Kategoriler";
                case "User":
                    return "Kullanıcılar";
                case "Options":
                    return "Ayarlar";
                case "Comment":
                    return "Yorumlar";
                case "Role":
                    return "Roller";
                default:
                    return controller;
            }
        }
    }
}