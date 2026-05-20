using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalAccount.Models;
using PersonalAccount.Services.Auth;
using PersonalAccount.Services.Email;
using PersonalAccount.Utils;

namespace PersonalAccount.Controllers;

public class EmailConfirmationController(IConfirmationTokenService confirmation, IEmailSender emailSender) : Controller
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
            return RedirectToAction("Error", "Home");
        
        return RedirectToAction("Index", "Cabinet");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public async Task<IActionResult> SendEmailConfirmation()
    {
        var studentId = User.GetId();
        if (studentId == null) return RedirectToAction("Error", "Home");
        var token = await confirmation.GenerateTokenAsync(studentId.Value);
        var confirmationUrl = Url.Action("Index", "EmailConfirmation", new
        {
            studentId, token
        }, Request.Scheme);

        await emailSender.SendEmailAsync("shamraev.alexandr@gmail.com", "Подтверждение почты", $"""
                                                                                                <head></head>
                                                                                                <body>
                                                                                                <a href="{confirmationUrl}">
                                                                                                Подтвердить почту</a>
                                                                                                </body>
                                                                                                """);

        return RedirectToAction("Index", "Cabinet");
    }
}