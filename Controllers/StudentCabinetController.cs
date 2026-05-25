using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalAccount.Services.Auth;
using PersonalAccount.Services.Cabinet;
using PersonalAccount.Types;
using PersonalAccount.Utils;
using PersonalAccount.ViewModels;

namespace PersonalAccount.Controllers;

[Authorize(Roles = AccountRoleConstants.Student)]
public class StudentCabinetController(IStudentCabinetService cabinet, IConfirmationTokenService confirmations)
    : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var accountEmail = User.GetEmail();
        var accountId = User.GetId();
        if (accountEmail is null || accountId is null)
            return RedirectToAction("Error", "Home");

        var student = await cabinet.GetStudentAsync(accountId.Value);
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