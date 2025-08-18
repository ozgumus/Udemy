using System.ComponentModel.DataAnnotations;

namespace dotnet_basic.Models;



public class RoleEditModel()
{
    public int Id { get; set; }

    
    [Required]
    [Display(Name = "Rol Adı")]
    public string RoleAdi { get; set; } = null!;
}

