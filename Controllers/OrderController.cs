using System.Threading.Tasks;
using dotnet_basic.Data;
using dotnet_basic.Models;
using dotnet_basic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_basic.Controllers;

public class OrderController : Controller
{
    private ICartService _cartService;
    private readonly DataContext _context;
    public OrderController(ICartService cartService, DataContext context)
    {
        _cartService = cartService;
        _context = context;
    }

    [Authorize]
    public async Task<ActionResult> Checkout()
    {
        //Uygulamaya giriş yapan kişinin kart bilgisinin alıyoruz. Bu bilgiyi view üzerinde kullanacağız.
        ViewBag.Cart = await _cartService.GetCart(User.Identity?.Name!);
        return View();
    }

    [HttpPost]
     public async Task<ActionResult> Checkout(OrderCreatModel model)
    {
        //Giriş yapan kullanıcı bilgilerini alıyoruz.
        var user = User.Identity?.Name!;

        // Eğer varsa kullanıcı kartını yoksa yeni bir card oluşturuyoruz.
        var cart = await _cartService.GetCart(user);

        //Eğer card boşsa sepetin boş olduğu bilgisini dönüyoruz.
        if (cart.CartItems.Count == 0)
        {
            ModelState.AddModelError("", "Sepetinizde ürün bulunmamaktadır.");
        }



        if (ModelState.IsValid)
        {
            //Formdan gönderilen verilen doğrulanmış bir şekilde gelimiş ise müşteri ve ürünlerini oluşturuyoruz.
            var order = new Order
            {
                AdSoyad = model.AdSoyad,
                Telefon = model.Telefon,
                AdresSatiri = model.AdresSatiri,
                PostaKodu = model.PostaKodu,
                Sehir = model.Sehir,
                SparisNotu = model.SparisNotu!,
                SparisTarihi = DateTime.Now,
                Toplamfiyat = cart.Toplam(),
                UserName = user,
                OrderItems = cart.CartItems.Select(i => new OrderItem
                {
                    UrunId = i.UrunId,
                    Fiyat = i.Urun.Fiyat,
                    Miktar = i.Miktar
                }).ToList()
            };

            //Oluşturulan müşteri bilgisi ve ürünlerini veri tabanına orders üzerine ekliyoruz.
            _context.Orders.Add(order);

            //Veri tabanına ürünler eklendiği için ürünler için oluşturulan cart bilgisinin veri tabanında gereksiz yere kalmaması için siliniyor.
            _context.Remove(cart);

            //Veri tabanına kaydediyoruz.
            await _context.SaveChangesAsync();

            //İşlem bittiği için yeni bir sayfaya yönlendiriyoruz ve ilgili spasrişin tamamlandığı mesajını vermek için ise OrderId bilgisini sayfaya gönderiyoruz. 
            return RedirectToAction("Completed", new { orderId = order.Id });
        }



        //Uygulamaya giriş yapan kişinin kart bilgisinin alıyoruz. Bu bilgiyi view üzerinde kullanacağız.
        ViewBag.Cart = await _cartService.GetCart(User.Identity?.Name!);
        return View();
    }


}