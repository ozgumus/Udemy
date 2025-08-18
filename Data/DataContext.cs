using dotnet_basic.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace dotnet_basic.Models;

public class DataContext : IdentityDbContext<AppUser, AppRole, int>
{
    // yapıcı metodlar(constructor methods)
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }
    public DbSet<Urun> Urunler { get; set; }
    public DbSet<Kategori> Kategoriler { get; set; }
    public DbSet<Slider> Sliderlar { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Slider>().HasData(
            new List<Slider>
            {
                new Slider{Id=1, Baslik="Slider Başlık 1", Aciklama="Slider 1 Aciklama", Resim="Slider-1.jpeg", Aktif=true, Index=0},
                new Slider{Id=2, Baslik="Slider Başlık 2", Aciklama="Slider 2 Aciklama", Resim="Slider-2.jpeg", Aktif=true, Index=1},
                new Slider{Id=3, Baslik="Slider Başlık 3", Aciklama="Slider 3 Aciklama", Resim="Slider-3.jpeg", Aktif=true, Index=2},
            }
        );

        modelBuilder.Entity<Kategori>().HasData(
            new List<Kategori>
            {
                new Kategori {Id=1,KategoriAdi="Telefon",Url="telefon"},
                new Kategori {Id=2,KategoriAdi="Elektronik",Url="elektronik"},
                new Kategori {Id=3,KategoriAdi="Beyaz Eşya",Url="beyaz-esya"},
                new Kategori {Id=4,KategoriAdi="Giyim",Url="giyim"},
                new Kategori {Id=5,KategoriAdi="Kozmetik",Url="kozmetik"},
                new Kategori {Id=6,KategoriAdi="Saat",Url="saat"},
                new Kategori {Id=7,KategoriAdi="Araba",Url="araba"},
                new Kategori {Id=8,KategoriAdi="Kitap",Url="kitap"},
                new Kategori {Id=9,KategoriAdi="Mobilya",Url="mobilya"}
            }

        );
        modelBuilder.Entity<Urun>().HasData(
            new List<Urun>()
            {
                new Urun(){Id=1, UrunAdi="Apple Watch 7",Fiyat=10000,Aktif=false, Resim="1.jpeg", Anasayfa=false, KategoriId=6, Aciklama="NEDEN APPLE WATCH SE SATIN ALMALISINIZ? — Motive ve aktif olmanız, sağlık durumunuzu takip etmeniz, bağlantıda ve güvende kalmanız için gereken her şey bileğinizde. watchOS 11 daha fazla yapay öğrenme, kişiselleştirme ve bağlantı özellikleri sunuyor. Düşme Algılama ve gelişmiş antrenman ölçümleri gibi özelliklere sahip Apple Watch SE, harika bir fiyatla sunuluyor."},
                new Urun(){Id=2, UrunAdi="Apple Watch 8",Fiyat=20000,Aktif=true,  Resim="2.jpeg", Anasayfa=true, KategoriId=6, Aciklama="NEDEN APPLE WATCH SE SATIN ALMALISINIZ? — Motive ve aktif olmanız, sağlık durumunuzu takip etmeniz, bağlantıda ve güvende kalmanız için gereken her şey bileğinizde. watchOS 11 daha fazla yapay öğrenme, kişiselleştirme ve bağlantı özellikleri sunuyor. Düşme Algılama ve gelişmiş antrenman ölçümleri gibi özelliklere sahip Apple Watch SE, harika bir fiyatla sunuluyor."},
                new Urun(){Id=3, UrunAdi="Apple Watch 9",Fiyat=30000,Aktif=false,  Resim="3.jpeg", Anasayfa=true, KategoriId=6, Aciklama="NEDEN APPLE WATCH SE SATIN ALMALISINIZ? — Motive ve aktif olmanız, sağlık durumunuzu takip etmeniz, bağlantıda ve güvende kalmanız için gereken her şey bileğinizde. watchOS 11 daha fazla yapay öğrenme, kişiselleştirme ve bağlantı özellikleri sunuyor. Düşme Algılama ve gelişmiş antrenman ölçümleri gibi özelliklere sahip Apple Watch SE, harika bir fiyatla sunuluyor."},
                new Urun(){Id=4, UrunAdi="Apple Watch 10",Fiyat=40000,Aktif=false,  Resim="4.jpeg", Anasayfa=true, KategoriId=6, Aciklama="NEDEN APPLE WATCH SE SATIN ALMALISINIZ? — Motive ve aktif olmanız, sağlık durumunuzu takip etmeniz, bağlantıda ve güvende kalmanız için gereken her şey bileğinizde. watchOS 11 daha fazla yapay öğrenme, kişiselleştirme ve bağlantı özellikleri sunuyor. Düşme Algılama ve gelişmiş antrenman ölçümleri gibi özelliklere sahip Apple Watch SE, harika bir fiyatla sunuluyor."},
                new Urun(){Id=5, UrunAdi="Apple Watch 11",Fiyat=50000,Aktif=true,  Resim="5.jpeg", Anasayfa=false, KategoriId=6, Aciklama="NEDEN APPLE WATCH SE SATIN ALMALISINIZ? — Motive ve aktif olmanız, sağlık durumunuzu takip etmeniz, bağlantıda ve güvende kalmanız için gereken her şey bileğinizde. watchOS 11 daha fazla yapay öğrenme, kişiselleştirme ve bağlantı özellikleri sunuyor. Düşme Algılama ve gelişmiş antrenman ölçümleri gibi özelliklere sahip Apple Watch SE, harika bir fiyatla sunuluyor."},
                new Urun(){Id=6, UrunAdi="Apple Watch 12",Fiyat=60000,Aktif=true,  Resim="6.jpeg", Anasayfa=true, KategoriId=6, Aciklama="NEDEN APPLE WATCH SE SATIN ALMALISINIZ? — Motive ve aktif olmanız, sağlık durumunuzu takip etmeniz, bağlantıda ve güvende kalmanız için gereken her şey bileğinizde. watchOS 11 daha fazla yapay öğrenme, kişiselleştirme ve bağlantı özellikleri sunuyor. Düşme Algılama ve gelişmiş antrenman ölçümleri gibi özelliklere sahip Apple Watch SE, harika bir fiyatla sunuluyor."},
                new Urun(){Id=7, UrunAdi="Apple Watch 13",Fiyat=70000,Aktif=false,  Resim="7.jpeg", Anasayfa=true, KategoriId=6, Aciklama="NEDEN APPLE WATCH SE SATIN ALMALISINIZ? — Motive ve aktif olmanız, sağlık durumunuzu takip etmeniz, bağlantıda ve güvende kalmanız için gereken her şey bileğinizde. watchOS 11 daha fazla yapay öğrenme, kişiselleştirme ve bağlantı özellikleri sunuyor. Düşme Algılama ve gelişmiş antrenman ölçümleri gibi özelliklere sahip Apple Watch SE, harika bir fiyatla sunuluyor."},
                new Urun(){Id=8, UrunAdi="Apple Watch 14",Fiyat=80000,Aktif=true,  Resim="8.jpeg", Anasayfa=false, KategoriId=6, Aciklama="NEDEN APPLE WATCH SE SATIN ALMALISINIZ? — Motive ve aktif olmanız, sağlık durumunuzu takip etmeniz, bağlantıda ve güvende kalmanız için gereken her şey bileğinizde. watchOS 11 daha fazla yapay öğrenme, kişiselleştirme ve bağlantı özellikleri sunuyor. Düşme Algılama ve gelişmiş antrenman ölçümleri gibi özelliklere sahip Apple Watch SE, harika bir fiyatla sunuluyor."}
            }
        );
    }

}


