using Blog.Shared.Utilities.Results.ComplexTypes;
using System;

namespace Blog.Shared.Entities.Abstract
{
    public abstract class DtoGetBase
    {
        public virtual ResultStatus ResultStatus { get; set; }
        public virtual string Message { get; set; }
        public virtual int CurrentPage { get; set; } = 1; // Şu an ki sayfası
        public virtual int PageSize { get; set; } = 5; // Bir sayfadaki içerik sayısı
        public virtual int TotalCount { get; set; } // Toplam içerik sayısı
        public virtual int TotalPages => (int)Math.Ceiling(decimal.Divide(TotalCount, PageSize)); // Toplam sayfa sayısı
        public virtual bool ShowPrevious => CurrentPage > 1; // Sayfa 1 den büyük ise bir önceki sayfaya gidebiliriz.
        public virtual bool ShowNext => CurrentPage < TotalPages; // Toplam sayfamızdan küçük ise bir sonraki sayfaya gidebiliriz.
        public virtual bool IsAscending { get; set; } = false;
    }
}