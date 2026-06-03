using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalAccount.Models.ViewModels;
using PersonalAccount.Services.Auth;
using PersonalAccount.Services.Cabinet;
using PersonalAccount.Types;
using PersonalAccount.Utils;

namespace PersonalAccount.Controllers;

[Authorize]
public class CabinetController(
    IStudentCabinetService studentCabinet,
    IAdminCabinetService adminCabinet,
    IConfirmationTokenService confirmations) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var accountId = User.GetId();
        var role = User.GetRole();

        if (accountId == null || role == null)
            return RedirectToAction("Error", "Home");

        switch (role)
        {
            case AccountRole.Student:
                return RedirectToAction("Student", new { accountId, role });
            case AccountRole.Administrator:
                return RedirectToAction("Admin", new { accountId, role });
            case AccountRole.Teacher:
            default:
                return RedirectToAction("Error", "Home");
        }
    }

    [HttpGet]
    public async Task<IActionResult> Student(int accountId, AccountRole role)
    {
        var accountEmail = User.GetEmail();
        if (accountEmail is null || role != AccountRole.Student)
            return RedirectToAction("Error", "Home");

        var student = await studentCabinet.GetStudentAsync(accountId);
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

    [HttpGet]
    public async Task<IActionResult> Admin(int accountId, AccountRole role)
    {
        if (role != AccountRole.Administrator)
            return RedirectToAction("Error", "Home");

        var accounts = await adminCabinet.GetAllStudentAccountsAsync();
        var profiles = await adminCabinet.GetAllStudentProfilesAsync();

        var confirmationStatuses = await Task.WhenAll(
            profiles.Select(p => confirmations.HasConfirmedTokensAsync(p.AccountId))
        );

        var studentInfos = profiles.Select((profile, i) => new StudentInfoViewModel
        {
            AccountId = profile.AccountId,
            Email = accounts[profile.AccountId].Email,
            FullName = profile.FullName,
            GroupName = profile.GroupName,
            PhotoUrl = profile.PhotoUrl?.ToString(),
            IsEmailConfirmed = confirmationStatuses[i],
        }).ToList();

        return View(new AdminCabinetViewModel
        {
            StudentInfos = studentInfos
        });
    }

    [HttpPost]
    [Authorize(Roles = "Administrator")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ConfirmStudentEmail(int id)
    {
        await adminCabinet.ConfirmStudentEmailAsync(id);
        var adminId = User.GetId();
        var role = User.GetRole();
        return RedirectToAction("Admin", new { accountId = adminId, role });
    }
}