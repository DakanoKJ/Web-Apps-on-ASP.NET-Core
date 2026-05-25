using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalAccount.Types;
using PersonalAccount.Utils;

namespace PersonalAccount.Controllers;

[Authorize]
public class CabinetController : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var role = User.GetRole();
        if (role is null) return Forbid();

        switch (role)
        {
            case AccountRoles.Student:
                return RedirectToAction("Index", "StudentCabinet");
            case AccountRoles.Administrator:
                return RedirectToAction("Index", "AdminCabinet");
            case AccountRoles.Teacher:
            default:
                return RedirectToAction("Error", "Home");
        }
    }
}