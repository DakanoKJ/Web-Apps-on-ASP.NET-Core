using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalAccount.Models;
using PersonalAccount.Models.Students;
using PersonalAccount.Services.Cabinet;
using PersonalAccount.Utils;

namespace PersonalAccount.Controllers;

[Authorize]
public class CabinetController(IStudentCabinetService cabinet, IPasswordService password) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var id = User.GetId();
        if (id is null) return RedirectToAction("Error", "Home");
        var student = await cabinet.GetStudentByIdAsync(id.Value);
        return View(student);
    }

    [HttpGet]
    public async Task<IActionResult> Edit()
    {
        var id = User.GetId();
        if (id is null) return RedirectToAction("Error", "Home");
        var student = await cabinet.GetStudentByIdAsync(id.Value);
        if (student is null) return RedirectToAction("Error", "Home");

        return View(new StudentEditViewModel
        {
            FullName = student.FullName,
            GroupName = student.GroupName,
            PhotoUrl = student.PhotoUrl?.ToString(),
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(StudentEditViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var id = User.GetId();
        if (id is null) return RedirectToAction("Error", "Home");

        await cabinet.UpdateByIdAsync(id.Value, new StudentModel
        {
            FullName = model.FullName,
            GroupName = model.GroupName,
            PhotoUrl = model.PhotoUrl?.ToUri(),
        });

        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult ChangePassword()
    {
        return View(new PasswordChangeViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangePassword(PasswordChangeViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var id = User.GetId();
        if (id is null) return RedirectToAction("Error", "Home");

        if (model.OldPassword == model.NewPassword)
        {
            ModelState.AddModelError(nameof(model.NewPassword), "Новый пароль не должен совпадать со старым");
            return View(model);
        }

        if (model.NewPassword != model.ConfirmNewPassword)
        {
            ModelState.AddModelError(nameof(model.ConfirmNewPassword), "Пароли не совпадают");
            return View(model);
        }

        var isValid = await password.ValidatePasswordAsync(id.Value, model.OldPassword);
        if (!isValid)
        {
            ModelState.AddModelError(nameof(model.OldPassword), "Неверный текущий пароль");
            return View(model);
        }

        await password.UpdatePasswordAsync(id.Value, model.NewPassword);
        return RedirectToAction("Index");
    }
}
