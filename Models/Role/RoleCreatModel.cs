using System.ComponentModel.DataAnnotations;

namespace dotnet_basic.Models;



public class RoleCreatModel()
{
    [Required]
    [Display(Name = "Rol Adı")]
    public string RoleAdi { get; set; } = null!;
}

