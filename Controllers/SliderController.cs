using System.Threading.Tasks;
using dotnet_basic.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_basic.Controllers;
[Authorize(Roles ="Admin")] 
public class SliderController : Controller
{
    private readonly DataContext _context;

    public SliderController(DataContext context)
    {
        _context = context;
    }

    public ActionResult Index()
    {
        var slider = _context.Sliderlar.ToList();


        return View(slider);
    }

    public ActionResult Creat()
    {
        return View();
    }
    [HttpPost]
    public async Task<ActionResult> Creat(SliderCreatModel model)
    {
        if (ModelState.IsValid)
        {
 
            //Random dosya adı belirleme
            var fileName = Path.GetRandomFileName() + ".jpg";
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await model.Resim!.CopyToAsync(stream);
            }

            var slider = new Slider
            {
                Id = model.Id,
                Baslik = model.Baslik,
                Aktif = model.Aktif,
                Index = model.Index,
                Resim = fileName
            };

            _context.Sliderlar.Add(slider);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        return View(model);
    }

    public IActionResult Edit(int id)
    {
        var entity = _context.Sliderlar.Select(i => new SliderEditModel
        {
            Id = i.Id,
            ResimAdi = i.Resim,
            Baslik = i.Baslik,
            Aciklama = i.Aciklama,
            Index = i.Index,
            Aktif = i.Aktif,
            Resim = i.Resim

        }).FirstOrDefault(i => i.Id == id);
        return View(entity);
    }
    [HttpPost]
    public async Task<IActionResult> Edit(int id, SliderEditModel model)
    {
        if (ModelState.IsValid)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            var slider = _context.Sliderlar.FirstOrDefault(i => i.Id == model.Id);

            if (slider != null)
            {
                if (model.ResimDosyasi != null)
                {
                    var fileName = Path.GetRandomFileName() + ".jpg";
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", fileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await model.ResimDosyasi!.CopyToAsync(stream);
                        slider.Resim = fileName;
                    }
                }
                slider.Aktif = model.Aktif;
                slider.Baslik = model.Baslik;
                slider.Aciklama = model.Aciklama;
                slider.Index = model.Index;
                _context.SaveChanges();
                TempData["EditMessage"] = $"{model.Baslik} güncellenmiştir.";
                return RedirectToAction("Index");
            }
        }
        return View(model);
    }

    public ActionResult Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var entity = _context.Sliderlar.Select(i => new SliderEditModel
        {
            Id = i.Id,
            ResimAdi = i.Resim,
            Baslik = i.Baslik,
            Aciklama = i.Aciklama,
            Index = i.Index,
            Aktif = i.Aktif,
            Resim = i.Resim

        }).FirstOrDefault(i => i.Id == id);
        return View(entity);

    }

    public ActionResult DeleteConfirm(int id)
    {
        var slider = _context.Sliderlar.FirstOrDefault(i => i.Id == id);
        _context.Sliderlar.Remove(slider!);
        _context.SaveChanges();



        TempData["EditMessage"] = $"{slider!.Baslik} veri tabanından silinmiştir.";
        return RedirectToAction("Index");
    }
}