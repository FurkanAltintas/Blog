namespace Blog.Services.Utilities
{
    public static class Messages
    {
        // Messages.Category.NotFound()

        public static class Category
        {
            public static string NotFound(bool isPlural) // true gelirse çoğul, false gelirse tekil.
            {
                if (isPlural) return "Hiç bir kategori bulunamadı";
                return "Böyle bir kategori bulunamadı";
            }

            public static string Add(string categoryName)
            {
                return $"{categoryName} adlı kategori başarıyla eklenmiştir.";
            }

            public static string Update(string categoryName)
            {
                return $"{categoryName} adlı kategori başarıyla güncellenmiştir.";
            }

            public static string Delete(string categoryName)
            {
                return $"{categoryName} adlı kategori başarıyla silinmiştir.";
            }

            public static string HardDelete(string categoryName)
            {
                return $"{categoryName} adlı kategori veritabanından silinmiştir.";
            }
        }

        public static class Article
        {
            public static string NotFound(bool isPlural)
            {
                if (isPlural) return "Hiç bir makale bulunamadı";
                return "Böyle bir makale bulunamadı";
            }

            public static string Add(string articleTitle)
            {
                return $"{articleTitle} adlı makale başarıyla eklenmiştir.";
            }

            public static string Update(string articleTitle)
            {
                return $"{articleTitle} adlı makale başarıyla güncellenmiştir.";
            }

            public static string Delete(string articleTitle)
            {
                return $"{articleTitle} adlı makale başarıyla silinmiştir.";
            }

            public static string HardDelete(string articleTitle)
            {
                return $"{articleTitle} adlı makale veritabanından silinmiştir.";
            }
        }
    }
}