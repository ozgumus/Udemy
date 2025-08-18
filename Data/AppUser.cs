using Microsoft.AspNetCore.Identity;

namespace dotnet_basic.Models;

public class AppUser : IdentityUser<int>
{
    public string AdSoyad { get; set; } = null!;
}
