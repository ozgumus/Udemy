using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

public class AcountLoginModel()
{
    [Required(ErrorMessage = "{0} alanı boş geçilemez")]
    [Display(Name = "Email")]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "{0} alanı boş geçilemez")]
    [Display(Name = "Password")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
    
    [Display(Name ="Beni Hatırla")]
    public bool BeniHatirla { get; set; }
}