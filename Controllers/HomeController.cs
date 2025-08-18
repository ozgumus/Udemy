using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using dotnet_basic.Models;

namespace dotnet_basic.Controllers;

public class HomeController : Controller
{

    private readonly DataContext _context;

    public HomeController(DataContext context)
    {
        _context = context;
    }

    public ActionResult Index()
    {
        var urunler = _context.Urunler.Where(i => i.Anasayfa && i.Aktif).ToList();
        ViewData["Kategoriler"] = _context.Kategoriler.ToList();
        return View(urunler);
    }
}
