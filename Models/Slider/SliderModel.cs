using System.ComponentModel.DataAnnotations;

namespace dotnet_basic.Models;

public class SliderModel
{public int Id { get; set; }


    [Display(Name = "Başlık")]
    [Required(ErrorMessage ="{0} bilgisi boş bırakılamaz.")]
    public string? Baslik { get; set; }


    [Display(Name = "Açıklama")]
    public string? Aciklama { get; set; }

    [Required(ErrorMessage ="Resim bilgisi boş bırakılamaz.")]
    [Display(Name = "Resim Adı")]
    public string Resim { get; set; } = null!;


    [Display(Name = "Index")]
    public int Index { get; set; }
    public bool Aktif { get; set; }
    
}