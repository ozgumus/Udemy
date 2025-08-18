using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

public class AccountUserResetPassword()
{
    public string Email { get; set; } = null!;
    public string Token { get; set; } = null!;

    [Required(ErrorMessage = "{0} alanı boş geçilemez")]
    [Display(Name = "Yeni Şifre")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Required(ErrorMessage ="{0} alanı boş geçilemez")]
    [Display(Name = "Yeni Şifre Tekrar")]
    [DataType(DataType.Password)]
    [Compare("Password",ErrorMessage ="Şifre uyuşmuyor")]
    public string ConfirmPassword { get; set; } = null!;

}