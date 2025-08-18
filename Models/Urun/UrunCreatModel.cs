using System.ComponentModel.DataAnnotations;

namespace dotnet_basic.Models;


public class UrunCreatModel : UrunModel
{
    [Display(Name = "Ürün Resmi")]
    public IFormFile? Resim { get; set; }


}