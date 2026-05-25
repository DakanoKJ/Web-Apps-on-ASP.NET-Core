using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalAccount.Services.Account;
using PersonalAccount.Services.Cabinet;
using PersonalAccount.Types;
using PersonalAccount.Utils;
using PersonalAccount.ViewModels;

namespace PersonalAccount.Controllers;

[Authorize(Roles = AccountRoleConstants.Student)]
public class StudentCabinetController(
    IStudentCabinetService cabinetService,
    IConfirmationTokenService confirmationTokenService)
    : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var accountEmail = User.GetEmail();
        var accountId = User.GetId();
        if (accountEmail is null || accountId is null)
            return RedirectToAction("Error", "Home");

        var student = await cabinetService.GetStudentAsync(accountId.Value);
        if (student is null) return RedirectToAction("Error", "Home");

        var isEmailConfirmed = await confirmationTokenService.HasConfirmedTokensAsync(student.AccountId);
        var group = student.GroupId == null ? null : await cabinetService.GetGroupAsync(student.GroupId.Value);

        return View(new StudentCabinetViewModel
        {
            Email = accountEmail,
            FullName = student.FullName,
            IsEmailConfirmed = isEmailConfirmed,
            PhotoUrl = student.PhotoUrl?.ToString(),
            GroupName = group?.Name ?? "Без группы",
        });
    }
}