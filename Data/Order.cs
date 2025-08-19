using dotnet_basic.Models;

namespace dotnet_basic.Data;

public class Order
{
    public int Id { get; set; }
    public DateTime SparisTarihi { get; set; }
    public string AdSoyad { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string Sehir { get; set; } = null!;
    public string AdresSatiri { get; set; } = null!;
    public string PostaKodu { get; set; } = null!;
    public string Telefon { get; set; } = null!;
    public string? SparisNotu { get; set; }
    public double Toplamfiyat { get; set; }
    public List<OrderItem> OrderItems { get; set; } = new();
     public double AraToplam()
    {
        return OrderItems.Sum(i => i.Fiyat * i.Miktar);
    }

    public double Toplam()
    {
        return OrderItems.Sum(i => i.Fiyat * i.Miktar) * 1.20;
    }

}

public class OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;
    public int UrunId { get; set; }
    public Urun Urun { get; set; } = null!;
    public double Fiyat { get; set; }
    public int Miktar { get; set; }
}