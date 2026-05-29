using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalAccount.Constants;
using PersonalAccount.Services.Cabinet;
using PersonalAccount.Utils;
using PersonalAccount.ViewModels;

namespace PersonalAccount.Controllers;

[Authorize(Roles = AccountRoleConstants.Teacher)]
public class TeacherCabinetController(ITeacherCabinetService cabinetService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var accountId = User.GetId();
        if (!accountId.HasValue) return Forbid();

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
            SubjectIdsOrder = subjectIdsOrder,
            SubjectInfos = subjectInfos,
            GroupsBySubjectInfos = groupsBySubjectInfos
        });
    }
}