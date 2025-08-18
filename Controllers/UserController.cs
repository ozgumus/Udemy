using System.Threading.Tasks;
using dotnet_basic.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace dotnet_basic.Controllers;

[Authorize(Roles = "Admin")]
public class UserController : Controller
{
    private UserManager<AppUser> _userManager;
    private RoleManager<AppRole> _roleManager;
    public UserController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }
    public async Task<ActionResult> Index(string role)
    {

        ViewBag.Roller = new SelectList(_roleManager.Roles, "Name", "Name", role);
        if (!string.IsNullOrEmpty(role))
        {
            return View(await _userManager.GetUsersInRoleAsync(role));
        }
        return View(_userManager.Users);
    }

    public ActionResult Creat()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> Creat(UserCreatModel model)
    {
        if (ModelState.IsValid)
        {

            var user = new AppUser { UserName = model.Email, AdSoyad = model.AdSoyad, Email = model.Email };
            var result = await _userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                TempData["EditMessage"] = $"{model.AdSoyad} kişisine ait kullanıcı veri tabanına başarılı bir şekilde kaydedilmiştir.";
                return RedirectToAction("Index");
            }
            foreach (var eror in result.Errors)
            {
                ModelState.AddModelError("", eror.Description);
            }
        }
        return View(model);
    }


    public async Task<ActionResult> Edit(string id)
    {
        var user = await _userManager.FindByIdAsync(id);

        if (user == null)
        {
            return RedirectToAction("Index");
        }

        ViewBag.Roles = await _roleManager.Roles.Select(i => i.Name).ToListAsync();

        if (id != null)
        {
            var result = new UserEditModel { AdSoyad = user!.AdSoyad, Email = user!.Email!, SelectedRols = await _userManager.GetRolesAsync(user) };
            return View(result);
        }
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> Edit(string id, UserEditModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.AdSoyad = model.AdSoyad;
                user.Email = model.Email;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded && !string.IsNullOrEmpty(model.Password))
                {
                    await _userManager.RemovePasswordAsync(user);
                    await _userManager.AddPasswordAsync(user, model.Password);
                }

                if (result.Succeeded)
                {
                    // İlk olarak mevcuttaki rollerini kaldırıyoruz.
                    await _userManager.RemoveFromRolesAsync(user, await _userManager.GetRolesAsync(user));
                    //Modelden gelen rol bilgisi boş mu dolu mu ona bakıyoruz.(Bu alan sorulabilir. Eğer model doluysa mevcuttakileri kaldırmak mantıklı olabilir.)
                    if (model.SelectedRols != null)
                    {   //Yeni rollerini tanımlıyoruz.
                        await _userManager.AddToRolesAsync(user, model.SelectedRols);
                    }
                    return RedirectToAction("Index");
                }

                foreach (var eror in result.Errors)
                {
                    ModelState.AddModelError("", eror.Description);
                }
            }
        }
        return View(model);
    }

    public async Task<ActionResult> Delete(string id)
    {
        if (id != null)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                return View(new UserDeleteModel
                {
                    Id = user.Id,
                    AdSoyad = user.AdSoyad
                });
            }
        }
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> Delete(int id, UserDeleteModel model)
    {
        if (id == model.Id)
        {
            var user = await _userManager.FindByIdAsync(model.Id.ToString());
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user!);
                if (result.Succeeded)
                {
                    TempData["EditMessage"] = $"{model.AdSoyad} isimli kullanıcı başarılı bir şekilde silinmiştir.";
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

}