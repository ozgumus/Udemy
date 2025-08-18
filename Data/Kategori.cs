namespace dotnet_basic.Models;

public class Kategori
{
    public int Id { get; set; }
    public String KategoriAdi { get; set; } = null!;
    public string Url { get; set; } = null!;

    public List<Urun> Uruns { get; set; } = new ();
}