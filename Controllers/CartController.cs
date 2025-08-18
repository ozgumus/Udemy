using System.Threading.Tasks;
using dotnet_basic.Models;
using dotnet_basic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace dotnet_basic.Controllers;


public class CartController : Controller
{
  private readonly DataContext _context;
  private readonly ICartService _cartService;

  public CartController(DataContext context, ICartService cartService)
  {
    _context = context;
    _cartService = cartService;
  }

  public async Task<ActionResult> Index()
  {
    var customerId = _cartService.GetCustomerId();
    var cart = await _cartService.GetCart(customerId);
    return View(cart);
  }

  [HttpPost]
  public async Task<ActionResult> AddToCart(int urunId, int miktar = 1)
  {

    #region İlgili alan service alanına taşındı
    // //İlgili alandaki açıklamalar alt kısımda AddToCart Notları ismi ile yer almaktadır.
    // var cart = await GetCart();
    // // var item = cart.CartItems.Where(i => i.UrunId == urunId).FirstOrDefault(); // İlgili alan alttaki kodla işlevsiz kaldı

    // //İçeriyue alınan urun ile uyuşan ürün veri tabanından alınıyor.
    // var urun = await _context.Urunler.FirstOrDefaultAsync(i => i.Id == urunId);

    // //Ürün veri tabanında olup olmadığı kontrol ediliyor.
    // if (urun != null)
    // {
    //   //Model içerisinde tanımlı olan model çağırılarak kart ekleme işlemi tamamlanıyor.
    //   cart.AddItem(urun, miktar);
    //   await _context.SaveChangesAsync();
    // }
    #endregion
    #region Model alanına taşındı
    // if (item != null)
    // {
    //   item.Miktar += 1;
    // }
    // else
    // {
    //   cart.CartItems.Add(new CartItem
    //   {
    //     UrunId = urunId,
    //     Miktar = miktar
    //   });
    // }
    #endregion


    await _cartService.AddToCart(urunId, miktar);
    return RedirectToAction("Index", "Home");
  }

  #region // AddToCart Notları
  // public async Task<ActionResult> AddToCart(int urunId, int miktar = 1)
  // {
  //   // Eğer şu an giriş yapan kişi bilgisi boş değilse isim bilgisini al
  //   var customerId = User.Identity?.Name;

  //   //Bir kullanıcı bilgi verisi oluşturacağız. Eğer kullanıcı daha önceden kayıt olmuşsa ilgili kullanıcı adına kart oluşturuluyor.
  //   var cart = await _context.Carts.Include(i => i.CartItems)//Mevcutta cart varsa içerisine cartitems içeriğini de ekliyoruz.
  //                                   .Where(i => i.CustomerId == customerId)//Eğer yukarıda sorguladağımuz giriş yapan kullanıcı veri tabanında varsa o kullanıcı bilgilerini alıyoruz.
  //                                   .FirstOrDefaultAsync();


  //   // Eğer giriş yapan kişi adına veri tabanında bir kart oluşturulmamışsa yeni bir kart oluşturuluyor.
  //   if (cart == null)
  //   {
  //     cart = new Cart { CustomerId = customerId! };
  //     _context.Carts.Add(cart);
  //   }

  //   // Şimdi ürün ekleme aşamasına geçebiliriz.
  //   // Eğer bir ürün daha önce eklenmişse CartItem üzerine ikinci defa eklendiğinde miktar kısmının artırılması için bir sorgulama yapacağız.

  //   //Bu alanda cart üzerine atılan ürünleri alıyoruz fakat bir sorgu ile. Function üzerine gönderilenm urunId ile uyuşan kayıt varsa bunu item içerisine alıyoruz.
  //   var item = cart.CartItems.Where(i => i.UrunId == urunId).FirstOrDefault();

  //   // Şimdi sorgulamamızı yapalım İtem içerisinde ilgili ürün daha önce atılmış mı atılmamış mı?
  //   if (item != null)
  //   {
  //     // Eğer ürün daha önce atılmışsa miktarı 1 artır.
  //     item.Miktar += 1;
  //   }
  //   else
  //   {
  //     // Eğer daha önce ilgili üründen yükleme yapılmamışsa yeni bir cartItem oluşturuyoruz.
  //     // CartItem dediğimiz şey kullanıcı/alınan ürün tablosunun kesiştiği bir yapı
  //     cart.CartItems.Add(new CartItem
  //     {
  //       UrunId = urunId,
  //       Miktar = miktar
  //     });
  //   }

  //   // Tüm bilgileri veri tabanına kaydetmek için
  //   await _context.SaveChangesAsync();
  //   return RedirectToAction("Index", "Home");
  // }

  #endregion

  public async Task<ActionResult> RemoveItem(int urunId, int miktar)
  {
    #region İlgili alan service üzerine taşınmıştır.
    // var cart = await GetCart();
    // var urun = await _context.Urunler.FirstOrDefaultAsync(i => i.Id == urunId);
    // if (urun != null)
    // {
    //   cart.DeleteItem(urunId, miktar);
    //   await _context.SaveChangesAsync();
    // }

    #endregion
    // var item = cart.CartItems.Where(i => i.CartItemId == cartItemId).FirstOrDefault(); // İlgili alan ihtiyaç kalmamıştır asıl kod model üzerine taşınmıştır.
    #region // İlgili alan model üzerine taşınmıştır.
    // if (item != null)
    // {
    //   cart.CartItems.Remove(item);
    //   await _context.SaveChangesAsync();
    //   TempData["EditMessage"] = $"Sepette bulunan {item.Urun.UrunAdi} ürün başarıyla silinmiştir.";
    // }
    #endregion
    await _cartService.RemoveItem(urunId, miktar);
    return RedirectToAction("Index", "Cart");
  }


  #region GetCart alanı service üzerine taşınmıştır.
  // private async Task<Cart> GetCart()
  // {
  //   //Soru işaretinden önceki kısım kullanıcı giriş yapmış ise kullanıcı bilgisi alıyor
  //   //Soru işareti sonrası ise giriş yapmamış kişinin cookie bilgisi varsa onu okuyor.
  //   //Bu iki bilgi var ise customerId içerisine bilgiyi kaydediyor. Eğer bilgi yoksa null bilgi veriyor.
  //   var customerId = User.Identity?.Name ?? Request.Cookies["customerId"];



  //   var cart = await _context.Carts.Include(i => i.CartItems).ThenInclude(i => i.Urun).Where(i => i.CustomerId == customerId).FirstOrDefaultAsync();
  //   if (cart == null)
  //   {
  //     // Kullanıcı giriş yapmış ise kullanıcı bilgisini alıyoruz.
  //     customerId = User.Identity?.Name;


  //     //CustomerId bilgisi eğer boşsa
  //     if (string.IsNullOrEmpty(customerId))
  //     {
  //       //Benzersiz bir cookie bilgisi oluşturuluyor.
  //       customerId = Guid.NewGuid().ToString();

  //       //Cookie ayarlarını giriyoruz
  //       var cookieOptions = new CookieOptions
  //       {
  //         //İlgili cookie bilgisi nekadar geçerli olacak
  //         Expires = DateTime.Now.AddMonths(1),
  //         IsEssential = true
  //       };

  //       // Eğer bir cookie bilgisi oluşturulmuşsa bu bilginin yazılması gerekiyor.
  //       // Bu bilgi response alanına yazılıyor çünkü ilk olarak oradan karşılanacak ve bilgi varsa oradan okunacak.
  //       Response.Cookies.Append("customerId", customerId, cookieOptions);
  //     }
  //     cart = new Cart { CustomerId = customerId };
  //     _context.Carts.Add(cart);
  //   }
  //   return cart;
  // }
  
  #endregion
}