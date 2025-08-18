using System.ComponentModel.DataAnnotations;

namespace dotnet_basic.Models;


public class UrunModel
{

    public int Id { get; set; }

    [Required(ErrorMessage ="Ürün adı bilgisini boş bırakmamalısınız.")]
    [Display(Name = "Ürün Adı")]
    [StringLength(50,ErrorMessage ="{0} için {2}-{1} karakter aralığında veri girmelisiniz",MinimumLength =3)]
    public string? UrunAdi { get; set; }


    [Display(Name = "Açıklamaı")]
    public string? Aciklama { get; set; }

    [Display(Name = "Ürün Fiyat")]
    [Required(ErrorMessage = "{0} bilgisi zorunludur.")]
    [Range(0,10000,ErrorMessage ="{0} alanına {1} ile {2} arasında bir tutar girmelisiniz.")]
    public double? Fiyat { get; set; }
    // Double tanımlamalarında Required tanımlaması yapmaya çalıştığında veri girilmediği zaman içerisine
    // boş bir string değer atmak ister fakat bunu double kabul etmez. Bu nedenle double yapısına "?" ilave ederek
    // sen boş olabilirsin diyoruz. Fakat bu controller kısmında veri gönderdiğimiz alanda soruna neden olacaktır.
    // Bu nedenle controller alanında da veri tanımladığımız alanda eğer boşsa 0 değerini otomatik olarak gönder diye
    // bilgi aktarımında bulunuyoruz.


    public bool Aktif { get; set; }
    public bool Anasayfa { get; set; }

    [Required(ErrorMessage ="Kategori bilgisi zorunlu alandır.")]
    [Display(Name = "Ürün Kategori ID")]
    public int? KategoriId { get; set; }

    
    public string? ResimAdi { get; set; }

    [Display(Name = "Ürün Resmi")]
    public IFormFile? ResimDosyasi { get; set; }


}