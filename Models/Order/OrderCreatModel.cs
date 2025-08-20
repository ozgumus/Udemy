using System.ComponentModel.DataAnnotations;

namespace dotnet_basic.Models;


public class OrderCreatModel
{
    [Display(Name = "Ad Soyad")]
    [Required(ErrorMessage = "Ad Soyad bilgisi boş bırakılamaz")]
    public string AdSoyad { get; set; } = null!;


    [Display(Name = "Şehir")]
    [Required(ErrorMessage = "Şehir bilgisi boş bırakılamaz")]
    public string Sehir { get; set; } = null!;


    [Display(Name = "Adres Bilgisi")]
    [Required(ErrorMessage = "Adres Bilgisi bilgisi boş bırakılamaz")]
    public string AdresSatiri { get; set; } = null!;


    [Display(Name = "Posta Kodu")]
    [Required(ErrorMessage = "Posta Kodu bilgisi boş bırakılamaz")]
    public string PostaKodu { get; set; } = null!;


    [Display(Name = "Telefon")]
    [Required(ErrorMessage = "Telefon bilgisi boş bırakılamaz")]
    public string Telefon { get; set; } = null!;


    public string? SparisNotu { get; set; }


    [Display(Name = "Kart Adı")]
    [Required(ErrorMessage = "Kart Adı bilgisi boş bırakılamaz")]
    public string CartName { get; set; } = null!;


    [Display(Name = "Kart Numarası")]
    [Required(ErrorMessage = "Kart Numarası bilgisi boş bırakılamaz")]
    public string CartNumber { get; set; } = null!;


    [Display(Name = "Kart Yılı")]
    [Required(ErrorMessage = "Kart Yılı bilgisi boş bırakılamaz")]
    public string CartExpirationYear { get; set; } = null!;


    [Display(Name = "Kart Ayı")]
    [Required(ErrorMessage = "Kart Ayı bilgisi boş bırakılamaz")]
    public string CartExpirationMont { get; set; } = null!;


    [Display(Name = "Kart Güvenlik Kodu")]
    [Required(ErrorMessage = "Kart Güvenlik Kodu bilgisi boş bırakılamaz")]
    public string CartCvc { get; set; } = null!;
}