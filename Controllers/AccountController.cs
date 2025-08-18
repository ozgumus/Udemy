using System.Security.Claims;
using System.Threading.Tasks;
using dotnet_basic.Models;
using dotnet_basic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace dotnet_basic.Controllers;

public class AccountController : Controller
{
    private UserManager<AppUser> _userManager;
    private SignInManager<AppUser> _signInManager;
    private IEmailService _emailService;
    private readonly DataContext _context;
    private readonly ICartService _cartService;
    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IEmailService emailService, DataContext context,ICartService cartService )
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _emailService = emailService;
        _context = context;
        _cartService = cartService;
    }
    public ActionResult Creat()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> Creat(AccountCreatModel model)
    {
        if (ModelState.IsValid)
        {
            // Yeni bir user tanımalama
            var user = new AppUser { UserName = model.Email, Email = model.Email, AdSoyad = model.AdSoyad };
            // Oluşturulan kullanıcının eklenmesi
            var result = await _userManager.CreateAsync(user, model.Password);
            // Kullanıcı oluşturulmasının başarı sonucunun alınması ve buna göre ilgili sayfaya yönlendirme
            if (result.Succeeded)
            {
                TempData["EditMessage"] = $"{model.AdSoyad} başarılı bir şekilde veri tabanına kaydedilmiştir.";
                return RedirectToAction("Index", "Home");
            }
            // Eğer başarılı değilse gelen hata mesajını hata mesajları arasına ekleyerek sayfada gösterme
            foreach (var i in result.Errors)
            {
                ModelState.AddModelError("", i.Description);
            }
        }
        return View(model);
    }

    public ActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> Login(AcountLoginModel model, string? returnUrl)
    {
        if (ModelState.IsValid)
        {
            // Kullanıcı bilgisini modelden gelen bilgi ile elde ediyoruz.
            var user = await _userManager.FindByEmailAsync(model.Email);

            // Artık kullanıcı ile ilgili kontroller yapılabilir.
            //Kullanıcı var mı yok mu?
            if (user != null)
            {
                //İlk olarak sistemden logout işlemi yapılır
                await _signInManager.SignOutAsync();
                //Eğer mail adresi sistemde kayıtlı ise o zaman şifre kontrol işlemi yapılır. son klısımdaki true alanı hatalı giriş anında program.cs alanında tanımlanan 5 dakika bekledikten sonra tekrar dene alanının aktif edilmesi için true yapılması gerekli.
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.BeniHatirla, true);

                if (result.Succeeded)
                {
                    await _userManager.ResetAccessFailedCountAsync(user);
                    await _userManager.SetLockoutEndDateAsync(user, null);

                    await _cartService.TransferCartToUser(user.UserName!);

                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return RedirectToAction(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");

                    }

                }
                else if (result.IsLockedOut)
                {
                    var lockoutDate = await _userManager.GetLockoutEndDateAsync(user);
                    var timeLeft = lockoutDate.Value - DateTime.UtcNow;
                    ModelState.AddModelError("", $"Hesabınız kilitlendi lütfen {timeLeft.Minutes + 1} dakika bekleyiniz");

                }
                else
                {
                    ModelState.AddModelError("", "Hatalı parola");
                }
            }
            else
            {
                // eğer mail adresi veri tabanında bulunamazsa hata koduna ilgili hata işlenir.
                ModelState.AddModelError("", $"{model.Email} adresi veri tabanında kayıtlı değildir.");
            }
        }
        return View(model);
    }

//     private async Task TransferCartToUser(AppUser user)
//     {
//         #region Service alanına taşındı.
//         // // Kullanıcıya ait urun ve cart bilgileri
//         // var userCart = await _context.Carts.Include(i => i.CartItems).ThenInclude(i => i.Urun).Where(i => i.CustomerId == user.UserName).FirstOrDefaultAsync();

//         // // Cookie ile oluşturulan sepet ve random kullanıcı bilgileri
//         // var cookieCart = await _context.Carts.Include(i => i.CartItems).ThenInclude(i => i.Urun).Where(i => i.CustomerId == Request.Cookies["customerId"]).FirstOrDefaultAsync();

//         // //Foreach ile cookie kart üzerindeki verileri dolaşacağız.

//         // if (cookieCart != null)
//         // {
//         //     foreach (var item in cookieCart.CartItems)
//         //     {
//         //         //İlk olarak ele alınacak ürün daha önceden giriş yapan kullanıcının sepetinde var mı yok mu sorgulaması yapmamız lazım.
//         //         //Bu nedenle il olarak bu cartItem'ı ele alalım.
//         //         //Aşağıdaki kodla eğer varsa giriş yapan kullanıcının cartItem yani aldığı ürünler listesini ele alıyoruz.     
//         //         var cartItem = userCart?.CartItems.Where(i => i.UrunId == item.UrunId).FirstOrDefault();
//         //         if (cartItem != null)
//         //         {
//         //             //Eğer giriş yapan kişinin sepetinde olan ürün giriş yapmadan önce sepete eklenen ürünle uyuşuyor ise kayıtlı olan 
//         //             //kullanıcının ürünü üzerine ekleme yapılır.
//         //             cartItem.Miktar += item.Miktar;
//         //         }
//         //         else
//         //         {
//         //             //Eğer giriş yapan kullanıcının sepetindeki ürün ile eşleşen ürün değilse yeni bir kart oluşturularak eklenir.
//         //             userCart?.CartItems.Add(new CartItem { UrunId = item.UrunId, Miktar = item.Miktar });
//         //         }
//         //     }


//         //     //Cookie üzerindeki ürünleri kullanıcı üzerine aktardığımız için boşa çıkan bu bilgiyi veri tabanınından silmemiz gerekmektedir.
//         //     _context.Carts.Remove(cookieCart);
//         //     //Değişiklikleri veri tabanına kaydediyoruz. 
//         //     await _context.SaveChangesAsync();

//         // }
// #endregion

//     }

    [Authorize]
    public async Task<ActionResult> LogOut()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login", "Account");
    }


    [Authorize]
    public ActionResult Settings()
    {
        return View();
    }

    public ActionResult AccessDenied()
    {
        return View();
    }

    [Authorize]
    public async Task<ActionResult> EditUser()
    {
        var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userManager.FindByIdAsync(userID!);

        if (user == null)
        {
            return RedirectToAction("Login", "Accound");
        }
        return View(new AccountEditUSerModel
        {
            AdSoyad = user.AdSoyad,
            Email = user.Email!
        });
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult> EditUser(AccountEditUSerModel model)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByIdAsync(userId!);
            if (user == null)
            {
                return View(model);
            }
            user.AdSoyad = model.AdSoyad;
            user.Email = model.Email;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                TempData["EditMessage"] = $"{model.AdSoyad} kullanıcıya ait bilgiler güncellenmiştir.";
                return View();
            }
            foreach (var eror in result.Errors)
            {
                ModelState.AddModelError("", eror.Description);
            }
            return View(model);
        }

        return View(model);
    }


    [Authorize]
    public ActionResult ChangePassword()
    {
        return View();
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult> ChangePassword(AccountUserChangePassword model)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userManager.FindByIdAsync(userId!);
        if (ModelState.IsValid)
        {
            if (user != null)
            {
                var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.Password);
                if (result.Succeeded)
                {
                    TempData["EditMessage"] = $"Şifre güncelleme işlemi başarı ile gerçekleştirilmiştir.";
                }
                foreach (var eror in result.Errors)
                {
                    ModelState.AddModelError("", eror.Description);
                }
            }
        }
        return View();
    }

    public ActionResult ForgotPassword()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> ForgotPassword(string email)
    {
        if (ModelState.IsValid)
        {
            if (string.IsNullOrEmpty(email))
            {
                TempData["EditMessage"] = "Lütfen Eposta adresinizi giriniz.";
                return View();
            }
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                TempData["EditMessage"] = "Lütfen geçerli bir eposta giriniz.";
                return View();
            }

            // Eposta alanı
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var url = Url.Action("ResetPassword", "Account", new { userId = user.Id, token });
            var link = $"Şifre sıfırlamak için <a href='http://localhost:5233{url}'>buraya</a> tıklayınız.";
            await _emailService.SendEmailAsync(user.Email!, "Paralo Sıfırlama", link);



            TempData["EditMessage"] = "Email adresinize gönderilen link üzerinden şifrenizi güncelleyebilirisiniz.";
            return RedirectToAction("Login");

        }
        return View();
    }

    public async Task<ActionResult> ResetPassword(string userId, string token)
    {
        if (userId == null || token == null)
        {
            RedirectToAction("Login", "Account");
            TempData["EditMessage"] = "İlgili link aktif değildir. Tekrar şifre yenileme talebinde bulununuz.";
        }
        var user = await _userManager.FindByIdAsync(userId!);
        if (user == null)
        {
            RedirectToAction("Login", "Account");
            TempData["EditMessage"] = "Tekrar şifre yenileme talebinde bulununuz.";
        }
        var model = new AccountUserResetPassword
        {
            Email = user!.Email!,
            Token = token!
        };
        return View(model);

    }

    [HttpPost]
    public async Task<ActionResult> ResetPassword(AccountUserResetPassword model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return RedirectToAction("Login");
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            if (result.Succeeded)
            {
                TempData["EditMessage"] = "Şifre güncelleme işleminiz başarı ile gerçekleştirilmiştir.";
                return RedirectToAction("login");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
        return View(model);
    }

}