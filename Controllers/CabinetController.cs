using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalAccount.Models.Students;
using PersonalAccount.Services.Auth;
using PersonalAccount.Services.Cabinet;
using PersonalAccount.Utils;

namespace PersonalAccount.Controllers;

[Authorize]
public class CabinetController(IStudentCabinetService cabinet, IConfirmationTokenService confirmations) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var id = User.GetId();
        if  (id is null) return RedirectToAction("Error", "Home"); 
        var student = await cabinet.GetStudentByIdAsync(id.Value);
        if (student is null) return RedirectToAction("Error", "Home");
        var isEmailConfirmed = await confirmations.HasConfirmedTokensAsync(student.Id);
        return View(new StudentWithEmailConfirmedModel
        {
            Id = student.Id,
            Email = student.Email,
            GroupName =  student.GroupName,
            FullName = student.FullName,
            IsEmailConfirmed = isEmailConfirmed,
            PhotoUrl =  student.PhotoUrl,
        });
    }
}