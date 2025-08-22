using System.Threading.Tasks;
using dotnet_basic.Data;
using dotnet_basic.Models;
using dotnet_basic.Services;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace dotnet_basic.Controllers;

public class OrderController : Controller
{
    private ICartService _cartService;
    private readonly DataContext _context;
    private UserManager<AppUser> _userManager;
    private readonly IConfiguration _configuration;
    public OrderController(ICartService cartService, DataContext context, UserManager<AppUser> userManager, IConfiguration configuration)
    {
        _cartService = cartService;
        _context = context;
        _userManager = userManager;
        _configuration = configuration;
    }

    [Authorize(Roles = "Admin")]
    public ActionResult Index()
    {
        return View(_context.Orders.ToList());
    }

    [Authorize(Roles = "Admin")]
    public ActionResult Details(int id)
    {
        var order = _context.Orders
             .Include(i => i.OrderItems)
             .ThenInclude(i => i.Urun)
             .FirstOrDefault(i => i.Id == id);

        return View(order);
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
                OrderItems = cart.CartItems.Select(i => new Data.OrderItem
                {
                    UrunId = i.UrunId,
                    Fiyat = i.Urun.Fiyat,
                    Miktar = i.Miktar
                }).ToList()
            };

            var payment = await ProcessPayment(model, cart);

            if (payment.Status == "success")
            {
                //Oluşturulan müşteri bilgisi ve ürünlerini veri tabanına orders üzerine ekliyoruz.
                _context.Orders.Add(order);

                //Veri tabanına ürünler eklendiği için ürünler için oluşturulan cart bilgisinin veri tabanında gereksiz yere kalmaması için siliniyor.
                _context.Remove(cart);

                //Veri tabanına kaydediyoruz.
                await _context.SaveChangesAsync();

                //İşlem bittiği için yeni bir sayfaya yönlendiriyoruz ve ilgili spasrişin tamamlandığı mesajını vermek için ise OrderId bilgisini sayfaya gönderiyoruz. 
                return RedirectToAction("Completed", new { orderId = order.Id });
            }
            else
            {
                ModelState.AddModelError("", payment.ErrorMessage);
            }

        }
        //Uygulamaya giriş yapan kişinin kart bilgisinin alıyoruz. Bu bilgiyi view üzerinde kullanacağız.
        ViewBag.Cart = cart;
        return View(model);
    }

    public ActionResult Completed(string orderId)
    {
        return View("Completed", orderId);
    }

    public async Task<ActionResult> OrderList()
    {
        var userName = User.Identity?.Name;
        var orders = await _context.Orders.Include(i => i.OrderItems).ThenInclude(i => i.Urun).Where(i => i.UserName == userName).ToListAsync();
        return View(orders);
    }

    private async Task<Payment> ProcessPayment(OrderCreatModel model, Cart cart)
    {
        Options options = new Options();
        options.ApiKey = _configuration["PaymentAPI:APIKey"];
        options.SecretKey = _configuration["PaymentAPI:SecretKey"];
        options.BaseUrl = "https://sandbox-api.iyzipay.com";

        CreatePaymentRequest request = new CreatePaymentRequest();
        request.Locale = Locale.TR.ToString();
        request.ConversationId = Guid.NewGuid().ToString();
        request.Price = cart.AraToplam().ToString();
        request.PaidPrice = cart.AraToplam().ToString();
        request.Currency = Currency.TRY.ToString();
        request.Installment = 1;
        request.BasketId = "B67832";
        request.PaymentChannel = PaymentChannel.WEB.ToString();
        request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

        PaymentCard paymentCard = new PaymentCard();
        paymentCard.CardHolderName = model.CartName;
        paymentCard.CardNumber = model.CartNumber;
        paymentCard.ExpireMonth = model.CartExpirationMont;
        paymentCard.ExpireYear = model.CartExpirationYear;
        paymentCard.Cvc = model.CartCvc;
        paymentCard.RegisterCard = 0;
        request.PaymentCard = paymentCard;

        Buyer buyer = new Buyer();
        buyer.Id = User.Identity?.Name;
        buyer.Name = "model.AdSoyad";
        buyer.Surname = "Doe";
        buyer.GsmNumber = model.Telefon;
        buyer.Email = "ozgumusmmt@gmail.com";
        buyer.IdentityNumber = "74300864791";
        buyer.LastLoginDate = "2015-10-05 12:43:35";
        buyer.RegistrationDate = "2013-04-21 15:12:09";
        buyer.RegistrationAddress = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
        buyer.Ip = "85.34.78.112";
        buyer.City = model.Sehir;
        buyer.Country = "Turkey";
        buyer.ZipCode = model.PostaKodu;
        request.Buyer = buyer;

        Address address = new Address();
        address.ContactName = model.AdSoyad;
        address.City = model.Sehir;
        address.Country = "Turkey";
        address.Description = model.AdresSatiri;
        address.ZipCode = model.PostaKodu;
        request.ShippingAddress = address;



        // Address billingAddress = new Address();
        // billingAddress.ContactName = "Jane Doe";
        // billingAddress.City = "Istanbul";
        // billingAddress.Country = "Turkey";
        // billingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
        // billingAddress.ZipCode = "34742";
        request.BillingAddress = address;


        List<BasketItem> basketItems = new List<BasketItem>();
        foreach (var i in cart.CartItems)
        {
            BasketItem firstBasketItem = new BasketItem();
            firstBasketItem.Id = i.CartItemId.ToString();
            firstBasketItem.Name = i.Urun.UrunAdi;
            firstBasketItem.Category1 = "Diğer";
            firstBasketItem.Category2 = "Accessories";
            firstBasketItem.ItemType = BasketItemType.PHYSICAL.ToString();
            firstBasketItem.Price = i.Urun.Fiyat.ToString();
            basketItems.Add(firstBasketItem);
        }
        request.BasketItems = basketItems;

        return await Payment.Create(request, options);
    }

}