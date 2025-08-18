using System.Threading.Tasks;
using dotnet_basic.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace dotnet_basic.Controllers;

[Authorize(Roles ="Admin")] 
public class RoleController : Controller
{
  private RoleManager<AppRole> _roleManager;

  public RoleController(RoleManager<AppRole> roleManager)
  {
    _roleManager = roleManager;
  }

  public ActionResult Index()
  {

    return View(_roleManager.Roles);
  }

  public ActionResult Creat()
  {
    return View();
  }

  [HttpPost]
  public async Task<ActionResult> Creat(RoleCreatModel model)
  {
    if (ModelState.IsValid)
    {
      var role = await _roleManager.CreateAsync(new AppRole { Name = model.RoleAdi });
      if (role.Succeeded)
      {
        return RedirectToAction("Index");
      }
      foreach (var item in role.Errors)
      {
        ModelState.AddModelError("", item.Description);
      }
    }
    return View(model);
  }

  public async Task<ActionResult> Edit(string id)
  {
    if (id != null)
    {
      var user = await _roleManager.FindByIdAsync(id);
      return View(new RoleEditModel { Id = user!.Id, RoleAdi = user.Name! });
    }
    return RedirectToAction("Index");
  }

  [HttpPost]
  public async Task<ActionResult> Edit(string id, RoleEditModel model)
  {
    if (ModelState.IsValid)
    {
      var entity = await _roleManager.FindByIdAsync(id);
      if (entity != null)
      {
        entity.Name = model.RoleAdi;
        var result = await _roleManager.UpdateAsync(entity);
        if (result.Succeeded)
        {
          return RedirectToAction("Index");
        }
        foreach (var error in result.Errors)
        {
          ModelState.AddModelError("", error.Description);
        }
      }
    }
    return View(model);
  }

  public async Task<ActionResult> Delete(string? id)
  {
    if (id != null)
    {
      var role = await _roleManager.FindByIdAsync(id);
      return View(new RoleEditModel{Id=role!.Id, RoleAdi=role.Name!});
    }
    return View();
  }

  public async Task<ActionResult> DeleteConfirm(int id, RoleEditModel model)
  {
    if (id == model.Id)
    {
      var role = await _roleManager.FindByIdAsync(model.Id.ToString());

      if (role != null)
      {
        var result = await _roleManager.DeleteAsync(role!);
        if (result.Succeeded)
        {
          TempData["EditMessage"] = $"{role.Name} isimli rol başarıyla silindi.";
          return RedirectToAction("Index");
        }
        foreach (var eror in result.Errors)
        {
          ModelState.AddModelError("", eror.Description);
        }
        return View();
      }
    }
    return View();
  }

}