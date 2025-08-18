using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

public class UserDeleteModel()
{
    public int Id { get; set; }

    [Required(ErrorMessage = "{0} alanı boş geçilemez")]
    [Display(Name = "Kullanıcı Adı")]
    // [RegularExpression("^[a-zA-Z0-9]$", ErrorMessage = "Geçersiz kullanıcı adı formatı.")]
    public string AdSoyad { get; set; } = null!;
    
}