using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalAccount.Services.Cabinet;
using PersonalAccount.Utils;

namespace PersonalAccount.Controllers;

[Authorize]
public class CabinetController(IStudentCabinetService cabinet) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var id = User.GetId();
        if  (id is null) return RedirectToAction("Error", "Home"); 
        var student = await cabinet.GetStudentByIdAsync(id.Value);
        return View(student);
    }
}