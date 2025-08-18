using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

public class AccountUserChangePassword()
{
    [Required(ErrorMessage = "{0} alanı boş geçilemez")]
    [Display(Name = "Mevcut Password")]
    [DataType(DataType.Password)]
    public string OldPassword { get; set; } = null!;

    [Required(ErrorMessage = "{0} alanı boş geçilemez")]
    [Display(Name = "Yeni Password")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Required(ErrorMessage ="{0} alanı boş geçilemez")]
    [Display(Name = "Yeni Password Tekrar")]
    [DataType(DataType.Password)]
    [Compare("Password",ErrorMessage ="Şifre uyuşmuyor")]
    public string ConfirmPassword { get; set; } = null!;
}