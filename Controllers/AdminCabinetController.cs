using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalAccount.Services.Cabinet;
using PersonalAccount.Types;
using PersonalAccount.ViewModels;

namespace PersonalAccount.Controllers;

[Authorize(Roles = AccountRoleConstants.Administrator)]
public class AdminCabinetController(IAdminCabinetService cabinet) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var accounts = await cabinet.GetAllStudentAccountsAsync();
        var profiles = await cabinet.GetAllStudentProfilesAsync();

        var studentInfos = profiles.Select(profile => new AdminCabinetStudentInfoViewModel
        {
            Email = accounts[profile.AccountId].Email,
            FullName = profile.FullName,
            GroupName = profile.GroupName,
            PhotoUrl = profile.PhotoUrl?.ToString(),
        }).ToList();

        return View(new AdminCabinetViewModel
        {
            StudentInfos = studentInfos
        });
    }

    [HttpGet]
    public IActionResult AddStudent()
    {
        return View();
    }
}