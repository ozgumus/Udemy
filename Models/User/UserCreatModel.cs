using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

public class UserCreatModel()
{
    [Required(ErrorMessage ="{0} alanı boş geçilemez")]
    [Display(Name = "Kullanıcı Adı")]
    // [RegularExpression("^[a-zA-Z0-9]$", ErrorMessage = "Geçersiz kullanıcı adı formatı.")]
    public string AdSoyad { get; set; } = null!;

    [Required(ErrorMessage ="{0} alanı boş geçilemez")]
    [Display(Name = "Email")]
    [EmailAddress]
    public string Email { get; set; } = null!;
}