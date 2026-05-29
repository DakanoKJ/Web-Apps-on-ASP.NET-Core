using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalAccount.Types;
using PersonalAccount.Utils;

namespace PersonalAccount.Controllers;

[Authorize]
public class CabinetController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        var role = User.GetRole();
        if (role is null) return Forbid();

        return role switch
        {
            AccountRoles.Student => RedirectToAction("Index", "StudentCabinet"),
            AccountRoles.Administrator => RedirectToAction("Index", "AdminCabinet"),
            AccountRoles.Teacher => RedirectToAction("Index", "TeacherCabinet"),
            _ => RedirectToAction("Error", "Home")
        };
    }
}