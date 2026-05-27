using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalAccount.Models;
using PersonalAccount.Services.Account;
using PersonalAccount.Services.Cabinet;
using PersonalAccount.Services.Email;
using PersonalAccount.Types;
using PersonalAccount.ViewModels;

namespace PersonalAccount.Controllers;

[Authorize(Roles = AccountRoleConstants.Administrator)]
public class AdminCabinetController(
    IAdminCabinetService cabinetService,
    IAccountService accountService,
    IEmailSenderService emailSenderService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var accounts = await cabinetService.GetAllStudentAccountsAsync();
        var profiles = await cabinetService.GetAllStudentProfilesAsync();
        var groups = await cabinetService.GetAllGroupsAsync();

        groups.Add(-1, new GroupModel
        {
            Name = "Без группы"
        });

        var groupInfos = profiles.GroupBy(profile => profile.GroupId)
            .ToDictionary(students =>
                {
                    var group = groups[students.Key ?? -1];
                    return new AdminCabinetGroupInfoViewModel
                    {
                        Name = group.Name,
                        Description = group.Description,
                        ImageUrl = group.ImageUrl?.ToString()
                    };
                },
                students => students.Select(student => new AdminCabinetStudentInfoViewModel
                {
                    Email = accounts[student.AccountId].Email,
                    FullName = student.FullName,
                    PhotoUrl = student.PhotoUrl?.ToString()
                }).ToList());


        return View(new AdminCabinetViewModel
        {
            GroupInfos = groupInfos
        });
    }

    [HttpGet]
    public IActionResult RegisterStudent()
    {
        return View(new RegisterStudentViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> RegisterStudent(RegisterStudentViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var hasAccountWithEmail = await accountService.HasAccountWithEmailAsync(model.Email);
        if (hasAccountWithEmail)
        {
            ModelState.AddModelError(string.Empty, $"Account with same email \"{model.Email}\" already exists");
            return View(model);
        }

        var password = await accountService.AddAccountWithGeneratedPasswordAsync(model.Email, AccountRoles.Student);
        await cabinetService.AddStudentProfileAsync(model.Email, model.FullName);

        var appUrl = Url.Action("Index", "Cabinet", Request.Scheme);

        await emailSenderService.SendEmailAsync(model.ContactEmail, "Данные для входа в аккаунт", $"""
             <head></head>
             <body>
                <a href="{appUrl}">Вход в аккаунт</a>
                <p>{model.Email}</p>
                <p>{password}</p>
             </body>
             """);

        return RedirectToAction("Index");
    }
}