﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Blog.Entities.Dtos
{
    public class UserPasswordChangeDto
    {
        [DisplayName("Şu Anki Şifreniz")]
        [Required(ErrorMessage = "{0} boş geçilmemelidir.")]
        [MaxLength(30, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır.")]
        [MinLength(6, ErrorMessage = "{0} {1} karakterden küçük olmamalıdır.")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [DisplayName("Yeni Şifreniz")]
        [Required(ErrorMessage = "{0} boş geçilmemelidir.")]
        [MaxLength(30, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır.")]
        [MinLength(6, ErrorMessage = "{0} {1} karakterden küçük olmamalıdır.")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DisplayName("Yeni Şifrenizin Tekrarı")]
        [Required(ErrorMessage = "{0} boş geçilmemelidir.")]
        [MaxLength(30, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır.")]
        [MinLength(6, ErrorMessage = "{0} {1} karakterden küçük olmamalıdır.")]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword), ErrorMessage = "Girmiş olduğunuz Yeni Şifreniz ile Yeni Şifrenizin Tekrarı alanları birbiri ile uyuşmalıdır.")] // NewPassword ile karşılaştırılacak
        public string RepeatPassword { get; set; }
    }
}