using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalAccount.Models.ViewModels;
using PersonalAccount.Services.Auth;
using PersonalAccount.Services.Cabinet;
using PersonalAccount.Types;
using PersonalAccount.Utils;

namespace PersonalAccount.Controllers;

[Authorize]
public class CabinetController(IStudentCabinetService cabinet, IConfirmationTokenService confirmations) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Student()
    {
        var accountId = User.GetId();
        var accountEmail = User.GetEmail();
        var role = User.GetRole();
        if (accountId is null || accountEmail is null || role != AccountRole.Student)
            return RedirectToAction("Error", "Home");

        var student = await cabinet.GetStudentByAccountIdAsync(accountId.Value);
        if (student is null) return RedirectToAction("Error", "Home");

        var isEmailConfirmed = await confirmations.HasConfirmedTokensAsync(student.Id);

        return View(new StudentCabinetViewModel
        {
            Email = accountEmail,
            GroupName = student.GroupName,
            FullName = student.FullName,
            IsEmailConfirmed = isEmailConfirmed,
            PhotoUrl = student.PhotoUrl?.ToString(),
        });
    }
}