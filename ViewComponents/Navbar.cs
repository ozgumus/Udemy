using dotnet_basic.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_basic.ViewComponents;

public class Navbar : ViewComponent
{
    private readonly DataContext _context;

    public Navbar(DataContext context)
    {
        _context = context;
    }

    public IViewComponentResult Invoke()
    {
        return View(_context.Kategoriler.ToList());
    }


}

