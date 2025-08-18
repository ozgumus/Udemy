using System.ComponentModel.DataAnnotations;

namespace dotnet_basic.Models;

public class SliderCreatModel : SliderModel
{
    public int Id { get; set; }

    [Required]
    [Display(Name = "Resim")]
    public IFormFile Resim { get; set; } = null!;
    

    [Required]
    [Display(Name = "Başlık")]
    public string? Baslik { get; set; }
   
}