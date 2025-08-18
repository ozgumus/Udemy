using System.ComponentModel.DataAnnotations;

namespace dotnet_basic.Models;


public class SliderEditModel:SliderModel

{   
    
    [Display(Name = "Resim Adı")]
    public String ResimAdi { get; set; } = null!;

    [Display(Name = "Resim Dosyası")]
    public IFormFile? ResimDosyasi { get; set; }
}