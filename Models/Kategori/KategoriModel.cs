using System.ComponentModel.DataAnnotations;

namespace dotnet_basic.Models;

public class KategoriModel
{
    public int Id { get; set; }

    [Required]
    [StringLength(30)]
    [Display(Name = "Kategori Adı")]
    public String KategoriAdi { get; set; } = null!;

    [Required]
    [StringLength(30)]
    [Display(Name = "URL")]
    public string Url { get; set; } = null!;
    public int UrunSayisi { get; set; }
}
