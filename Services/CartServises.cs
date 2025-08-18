using dotnet_basic.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_basic.Services;

public interface ICartService
{
    string GetCustomerId();
    Task<Cart> GetCart(string customerId);
    Task AddToCart(int urunId, int miktar = 1);
    Task RemoveItem(int urunId, int miktar = 1);
    Task TransferCartToUser(string username);
}

public class CartService(DataContext context, IHttpContextAccessor httpContextAccessor) : ICartService
{

    private readonly DataContext _context = context;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
  

    public async Task AddToCart(int urunId, int miktar = 1)
    {

        var cart = await GetCart(GetCustomerId());
        // var item = cart.CartItems.Where(i => i.UrunId == urunId).FirstOrDefault(); // İlgili alan alttaki kodla işlevsiz kaldı

        //İçeriyue alınan urun ile uyuşan ürün veri tabanından alınıyor.
        var urun = await _context.Urunler.FirstOrDefaultAsync(i => i.Id == urunId);

        //Ürün veri tabanında olup olmadığı kontrol ediliyor.
        if (urun != null)
        {
            //Model içerisinde tanımlı olan model çağırılarak kart ekleme işlemi tamamlanıyor.
            cart.AddItem(urun, miktar);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<Cart> GetCart(string custId)
    {
        //Soru işaretinden önceki kısım kullanıcı giriş yapmış ise kullanıcı bilgisi alıyor
        //Soru işareti sonrası ise giriş yapmamış kişinin cookie bilgisi varsa onu okuyor.
        //Bu iki bilgi var ise customerId içerisine bilgiyi kaydediyor. Eğer bilgi yoksa null bilgi veriyor.
        // var customerId = User.Identity?.Name ?? Request.Cookies["customerId"];
        var cart = await _context.Carts.Include(i => i.CartItems).ThenInclude(i => i.Urun).Where(i => i.CustomerId == custId).FirstOrDefaultAsync();
        if (cart == null)
        {
            // Kullanıcı giriş yapmış ise kullanıcı bilgisini alıyoruz.
            var customerId = _httpContextAccessor.HttpContext?.User.Identity?.Name;
            //CustomerId bilgisi eğer boşsa
            if (string.IsNullOrEmpty(customerId))
            {
                //Benzersiz bir cookie bilgisi oluşturuluyor.
                customerId = Guid.NewGuid().ToString();

                //Cookie ayarlarını giriyoruz
                var cookieOptions = new CookieOptions
                {
                    //İlgili cookie bilgisi nekadar geçerli olacak
                    Expires = DateTime.Now.AddMonths(1),
                    IsEssential = true
                };
                // Eğer bir cookie bilgisi oluşturulmuşsa bu bilginin yazılması gerekiyor.
                // Bu bilgi response alanına yazılıyor çünkü ilk olarak oradan karşılanacak ve bilgi varsa oradan okunacak.
                _httpContextAccessor.HttpContext?.Response.Cookies.Append("customerId", customerId, cookieOptions);
            }
            cart = new Cart { CustomerId = customerId };
            _context.Carts.Add(cart);
        }
        return cart;
    }

    public string GetCustomerId()
    {
        var context = _httpContextAccessor.HttpContext;
        return context?.User.Identity?.Name ?? context?.Request.Cookies["customerId"]!;
    }   

    public async Task RemoveItem(int urunId, int miktar = 1)
    {
         var cart = await GetCart(GetCustomerId());
        var urun = await _context.Urunler.FirstOrDefaultAsync(i => i.Id == urunId);
        if (urun != null)
        {
            cart.DeleteItem(urunId, miktar);
            await _context.SaveChangesAsync();
        }
    }

    public async Task TransferCartToUser(string username)
    {
        //Bu alan önceden controller üzerindeydi
        // Kullanıcıya ait urun ve cart bilgileri
        // Bu alana controller kısmında gerek vardı bir altındaki kod ile değiştirildi.
        // var userCart = await _context.Carts.Include(i => i.CartItems).ThenInclude(i => i.Urun).Where(i => i.CustomerId == user.UserName).FirstOrDefaultAsync();
        var userCart = await GetCart(username);

        // Cookie ile oluşturulan sepet ve random kullanıcı bilgileri
        // Bu alana controller kısmında gerek vardı bir altındaki kod ile değiştirildi.
        // var cookieCart = await _context.Carts.Include(i => i.CartItems).ThenInclude(i => i.Urun).Where(i => i.CustomerId == Request.Cookies["customerId"]).FirstOrDefaultAsync();
        var cookieCart = await GetCart(_httpContextAccessor.HttpContext!.Request.Cookies["customerId"]!);
        

        //Foreach ile cookie kart üzerindeki verileri dolaşacağız.

        if (cookieCart != null)
        {
            foreach (var item in cookieCart.CartItems)
            {
                //İlk olarak ele alınacak ürün daha önceden giriş yapan kullanıcının sepetinde var mı yok mu sorgulaması yapmamız lazım.
                //Bu nedenle ilk olarak bu cartItem'ı ele alalım.
                //Aşağıdaki kodla eğer varsa giriş yapan kullanıcının cartItem yani aldığı ürünler listesini ele alıyoruz.     
                var cartItem = userCart?.CartItems.Where(i => i.UrunId == item.UrunId).FirstOrDefault();
                if (cartItem != null)
                {
                    //Eğer giriş yapan kişinin sepetinde olan ürün giriş yapmadan önce sepete eklenen ürünle uyuşuyor ise kayıtlı olan 
                    //kullanıcının ürünü üzerine ekleme yapılır.
                    cartItem.Miktar += item.Miktar;
                }
                else
                {
                    //Eğer giriş yapan kullanıcının sepetindeki ürün ile eşleşen ürün değilse yeni bir kart oluşturularak eklenir.
                    userCart?.CartItems.Add(new CartItem { UrunId = item.UrunId, Miktar = item.Miktar });
                }
            }
            //Cookie üzerindeki ürünleri kullanıcı üzerine aktardığımız için boşa çıkan bu bilgiyi veri tabanınından silmemiz gerekmektedir.
            _context.Carts.Remove(cookieCart);
            //Değişiklikleri veri tabanına kaydediyoruz. 
            await _context.SaveChangesAsync();
        }
    }
}