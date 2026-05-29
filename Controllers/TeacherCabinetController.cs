using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalAccount.Constants;
using PersonalAccount.Services.Account;
using PersonalAccount.Services.Cabinet;
using PersonalAccount.Utils;
using PersonalAccount.ViewModels;

namespace PersonalAccount.Controllers;

[Authorize(Roles = AccountRoleConstants.Teacher)]
public class TeacherCabinetController(
    ITeacherCabinetService cabinetService,
    IConfirmationTokenService confirmationTokenService
) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var accountId = User.GetId();
        var accountEmail = User.GetEmail();
        if (accountId == null || accountEmail == null) return Forbid();

        var profile = await cabinetService.GetTeacherProfileAsync(accountId.Value);
        if (profile == null) return RedirectToAction("Error", "Home");
        var isEmailConfirmed = await confirmationTokenService.HasConfirmedTokensAsync(accountId.Value);

        var links = await cabinetService.GetAllTeacherGroupSubjectsAsync(accountId.Value);
        var subjects = await cabinetService.GetAllSubjects(links);
        var groupsBySubjects = await cabinetService.GetAllGroupsBySubjects(links);

        var subjectIdsOrder = subjects.OrderBy(subject => subject.Name).Select(subject => subject.Id).ToList();
        var subjectInfos = subjects.ToDictionary(
            subject => subject.Id,
            subject => new TeacherCabinetSubjectInfoViewModel
            {
                Name = subject.Name,
            }
        );
        var groupsBySubjectInfos = groupsBySubjects.ToDictionary(
            groupsBySubject => groupsBySubject.Key,
            groupsBySubject => groupsBySubject.Value.Select(group => new TeacherCabinetGroupInfoViewModel
                {
                    Name = group.Name,
                    ImageUrl = group.ImageUrl?.ToString()
                }
            ).ToList()
        );

        return View(new TeacherCabinetViewModel
        {
            FullName = profile.FullName,
            Email = accountEmail,
            IsEmailConfirmed = isEmailConfirmed,
            SubjectIdsOrder = subjectIdsOrder,
            SubjectInfos = subjectInfos,
            GroupsBySubjectInfos = groupsBySubjectInfos
        });
    }
}