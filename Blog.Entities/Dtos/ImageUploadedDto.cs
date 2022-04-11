namespace Blog.Entities.Dtos
{
    public class ImageUploadedDto
    {
        public string FullName { get; set; } // Resmin yeni adı
        public string OldName { get; set; } // Resmin eski adı
        public string Extension { get; set; } // Uzantı
        public string Path { get; set; } // Resmin tam yolu
        public string FolderName { get; set; } // Hangi klasörde
        public long Size { get; set; } // Resmin boyutu
    }
}