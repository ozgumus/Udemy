using System.Threading.Tasks;
using dotnet_basic.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace dotnet_basic.Controllers;

[Authorize(Roles = "Admin")]
public class UrunController : Controller
{
    private readonly DataContext _context;
    public UrunController(DataContext context)
    {
        _context = context;
    }
    public ActionResult Index(int? kategori)
    {
        var query = _context.Urunler.AsQueryable();
        if (kategori != null)
        {
            query = query.Where(i => i.KategoriId == kategori);
        }

        var urunler = query.Select(i => new UrunGetModel
        {
            Id = i.Id,
            UrunAdi = i.UrunAdi,
            Fiyat = i.Fiyat,
            Aktif = i.Aktif,
            Anasayfa = i.Anasayfa,
            KategoriAdi = i.Kategori.KategoriAdi,
            ResimAdi = i.Resim
        }).ToList();

        ViewBag.kategoriler = new SelectList(_context.Kategoriler.ToList(), "Id", "KategoriAdi", kategori);
        return View(urunler);
    }

    [AllowAnonymous]    // Bu alan yukarı kısımda role ve giriş bilgisini zorunlu tutan alanı ezer. Genele koyulan yasağı yok sayar.
    public ActionResult List(string url, string q)
    {
        var query = _context.Urunler.Where(i => i.Aktif);
        if (!string.IsNullOrEmpty(url))
        {
            query = query.Where(i => i.Kategori.Url == url);

        }

        if (!string.IsNullOrEmpty(q))
        {
            query = query.Where(i => i.UrunAdi!.ToLower().Contains(q.ToLower()));
        }

        ViewData["q"] = q;
        // var urunler = _context.Urunler.Where(i => i.Aktif && i.Kategori.Url == url).ToList();
        return View(query.ToList());
    }
    [AllowAnonymous]
    public ActionResult Details(int id)
    {
        var urun = _context.Urunler.Find(id);
        if (urun == null)
        {
            return RedirectToAction("Index", "Home");
        }
        ViewData["BenzerUrunler"] = _context.Urunler.Where(i => i.Aktif && i.KategoriId == urun.KategoriId && i.Id != urun.Id).Take(4).ToList();
        return View(urun);
    }
    public ActionResult Creat()
    {

        //Örnek Gönderim
        // ViewData["kategori"] = _context.Kategoriler.ToList();
        ViewBag.kategoriler = new SelectList(_context.Kategoriler.ToList(), "Id", "KategoriAdi");

        return View();
    }
    [HttpPost]
    public async Task<ActionResult> Creat(UrunCreatModel urun)
    {
        if (urun.Resim == null || urun.Resim.Length == 0)
        {
            ModelState.AddModelError("Resim", "Resim yüklenmesi zorunludur.");

        }
        if (ModelState.IsValid)
        {

            //Random dosya adı belirleme
            var fileName = Path.GetRandomFileName() + ".jpg";
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await urun.Resim!.CopyToAsync(stream);
            }

            var eleman = new Urun
            {
                UrunAdi = urun.UrunAdi,
                Aciklama = urun.Aciklama,
                Fiyat = urun.Fiyat ?? 0, // burada "?? 0" ifadesi eğer içeril null gelirse sen veriye 0 değeri ata demiş oluyoruz.
                Aktif = urun.Aktif,
                Anasayfa = urun.Anasayfa,
                Resim = fileName, //Yukarıda random belirlenen dosya adı veri tabanındaki tabloya kaydedilecek isim olarak atandı
                KategoriId = (int)urun.KategoriId!,
                // Kategori = urun.Kategori
            };
            _context.Urunler.Add(eleman);
            _context.SaveChanges();
            return Redirect("List");
        }
        ViewBag.kategoriler = new SelectList(_context.Kategoriler.ToList(), "Id", "KategoriAdi");
        return View(urun);
    }
    public ActionResult Edit(int id)
    {
        if (ModelState.IsValid)
        {
            var entity = _context.Urunler.Select(i => new UrunEditModel
            {
                Id = i.Id,
                UrunAdi = i.UrunAdi,
                Aciklama = i.Aciklama,
                Fiyat = i.Fiyat,
                Aktif = i.Aktif,
                Anasayfa = i.Anasayfa,
                KategoriId = i.KategoriId,
                ResimAdi = i.Resim
            }).FirstOrDefault(i => i.Id == id);
            ViewBag.kategoriler = new SelectList(_context.Kategoriler.ToList(), "Id", "KategoriAdi");
            return View(entity);
        }
        ViewBag.kategoriler = new SelectList(_context.Kategoriler.ToList(), "Id", "KategoriAdi");
        return View();
    }
    [HttpPost]
    public async Task<ActionResult> Edit(int id, UrunEditModel model)
    {
        if (ModelState.IsValid)
        {
            if (id != model.Id)
            {
                return NotFound();
            }
            var urun = _context.Urunler.FirstOrDefault(i => i.Id == model.Id);

            if (urun != null)
            {
                if (model.ResimDosyasi != null)
                {
                    var filename = Path.GetRandomFileName() + ".jpg";
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", filename);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await model.ResimDosyasi!.CopyToAsync(stream);
                        urun.Resim = filename;
                    }
                }
                urun.UrunAdi = model.UrunAdi;
                urun.Aciklama = model.Aciklama;
                urun.Fiyat = model.Fiyat ?? 0;
                urun.Aktif = model.Aktif;
                urun.Anasayfa = model.Anasayfa;
                urun.KategoriId = (int)model.KategoriId!;
                urun.Resim = model.ResimAdi;
                _context.SaveChanges();
                TempData["EditMessage"] = $"{urun.UrunAdi} ürünü güncellendi.";
                return RedirectToAction("Index");
            }
            ;
            return RedirectToAction("Index");
        }
        ViewBag.kategoriler = new SelectList(_context.Kategoriler.ToList(), "Id", "KategoriAdi");
        return View(model);

    }

    public ActionResult Delete(int? id)
    {
        if (id == null)
        {
            return View();
        }

        var entity = _context.Urunler.Select(i => new UrunEditModel
        {
            Id = i.Id,
            UrunAdi = i.UrunAdi,
            Aciklama = i.Aciklama,
            Fiyat = i.Fiyat,
            Aktif = i.Aktif,
            Anasayfa = i.Anasayfa,
            KategoriId = i.KategoriId,
            ResimAdi = i.Resim
        }).FirstOrDefault(i => i.Id == id);
        ViewBag.kategoriler = new SelectList(_context.Kategoriler.ToList(), "Id", "KategoriAdi");
        return View(entity);
    }
    public ActionResult DeleteConfirm(UrunEditModel model)
    {
        if (model == null)
        {
            return NotFound();
        }
        var entity = _context.Urunler.FirstOrDefault(i => i.Id == model.Id);
        _context.Urunler.Remove(entity!);
        _context.SaveChanges();
        TempData["EditMessage"] = $"{model.UrunAdi} veri tabanından silinmiştir.";
        return RedirectToAction("Index");
    }


}