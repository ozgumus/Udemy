using dotnet_basic.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnet_basic.Controllers;

[Authorize(Roles ="Admin")]
public class KategoriController : Controller
{
    private readonly DataContext _context;

    public KategoriController(DataContext context)
    {
        _context = context;
    }

    public ActionResult Index()
    {
        // Include(i=> i.Uruns) kodu ile kategorilere bağlı tüm ürünleri listeleriz.
        // şu an bu işlemi sadece ürünlerin sayısını almak için yapıyoruz ama elverişli bir işlem değil.        
        var kategoriler = _context.Kategoriler.Select(i => new KategoriGetModel
        {
            Id = i.Id,
            KategoriAdi = i.KategoriAdi,
            Url = i.Url,
            UrunSayisi = i.Uruns.Count()
        }).ToList();
        return View(kategoriler);
    }

    public ActionResult Creat()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Creat(KategoriCreatModel model)
    {
        if (ModelState.IsValid)
        {

            var kCreat = new Kategori
            {
                KategoriAdi = model.KategoriAdi,
                Url = model.Url
            };

            _context.Kategoriler.Add(kCreat);
            _context.SaveChanges();

            return Redirect("Index");
        }
        return View(model);
    }

    public ActionResult Edit(int id)
    {
        var entity = _context.Kategoriler.Select(i => new KategoriEditModels
        {
            Id = i.Id,
            KategoriAdi = i.KategoriAdi,
            Url = i.Url
        }).FirstOrDefault(i => i.Id == id);

        return View(entity);
    }

    [HttpPost]
    public ActionResult Edit(int id, KategoriEditModels edit)
    // Gelecek olan id bilgisi formdan seçilen ürünün link üzerinden gelen id bilgisi
    // aynı zamanda model üzerinden de bir id bilgisi gelecek ve bunları karşılaştıracağız.
    {
        if (ModelState.IsValid)
        {

            if (id != edit.Id)
            {
                return NotFound();

            }
            var entity = _context.Kategoriler.FirstOrDefault(i => i.Id == edit.Id);

            if (entity != null)
            {
                entity.KategoriAdi = edit.KategoriAdi;
                entity.Url = edit.Url;

                _context.SaveChanges();
                TempData["EditMessage"] = $"{edit.KategoriAdi} güncellenmiştir.";
                return RedirectToAction("Index");
            }
        }

        return View(edit);
    }

    public ActionResult Delete(int? id)
    {
        if (id == null)
        {
            return RedirectToAction("Index");

        }
        var entity = _context.Kategoriler.Select(i => new KategoriEditModels
        {
            Id = i.Id,
            KategoriAdi = i.KategoriAdi,
            Url = i.Url
        }).FirstOrDefault(i => i.Id == id);
        // TempData["EditMessage"] = $"{edit.KategoriAdi} güncellenmiştir.";
        return View(entity);
    }


    [HttpPost]
    public ActionResult DeleteConfirm(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var entity = _context.Kategoriler.FirstOrDefault(i => i.Id == id);

        if (entity != null)
        {
            _context.Kategoriler.Remove(entity);
            _context.SaveChanges();
            TempData["EditMessage"] = $"{entity.KategoriAdi} silinmiştir.";
        }

        return RedirectToAction("Index");
    }


}