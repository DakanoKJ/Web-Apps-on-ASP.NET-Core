using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalAccount.Models;
using PersonalAccount.Services.Auth;
using PersonalAccount.Utils;

namespace PersonalAccount.Controllers;

public class EmailConfirmationController(IConfirmationTokenService confirmation) : Controller
{
    [HttpGet]
    public IActionResult Index(int studentId, string token)
    {
        return View(new EmailConfirmationViewModel
        {
            StudentId = studentId,
            Token = token
        });
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ConfirmEmail(EmailConfirmationViewModel model)
    {
        var confirmed = await confirmation.ValidateTokenAsync(model.StudentId,  model.Token);
        if (!confirmed)
            return BadRequest("Ссылка подтверждения недействительна или устарела.");
        
        return RedirectToAction("Index", "Cabinet");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public async Task<IActionResult> SendEmailConfirmation()
    {
        var studentId = User.GetId();
        if (studentId == null) return RedirectToAction("Error", "Home");
        var token = confirmation.GenerateTokenAsync(studentId.Value);
        var confirmationUrl = Url.Action("Index", "EmailConfirmation", new
        {
            studentId, token
        }, Request.Scheme);
        // TODO: Add Smtp service

        return RedirectToAction("Index", "Cabinet");
    }
}