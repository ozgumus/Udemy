using dotnet_basic.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_basic.ViewComponents;

public class Slider : ViewComponent
{
    private readonly DataContext _context;

    public Slider(DataContext context)
    {
        _context = context;
    }

    public IViewComponentResult Invoke()
    {
        return View(_context.Sliderlar.ToList());
    }
    
}