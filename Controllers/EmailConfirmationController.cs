using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalAccount.Models.ViewModels;
using PersonalAccount.Services.Auth;
using PersonalAccount.Services.Email;
using PersonalAccount.Utils;

namespace PersonalAccount.Controllers;

public class EmailConfirmationController(IConfirmationTokenService confirmation, IEmailSender emailSender) : Controller
{
    [HttpGet]
    public IActionResult Index(int accountId, string token)
    {
        return View(new EmailConfirmationViewModel
        {
            AccountId = accountId,
            Token = token
        });
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ConfirmEmail(EmailConfirmationViewModel model)
    {
        var confirmed = await confirmation.ValidateTokenAsync(model.AccountId,  model.Token);
        if (!confirmed)
            return RedirectToAction("Error", "Home");
        
        return RedirectToAction("Student", "Cabinet");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public async Task<IActionResult> SendEmailConfirmation()
    {
        var accountId = User.GetId();
        var studentEmail = User.GetEmail();
        if (accountId == null || studentEmail == null) return RedirectToAction("Error", "Home");
        var token = await confirmation.GenerateTokenAsync(accountId.Value);
        var confirmationUrl = Url.Action("Index", "EmailConfirmation", new
        {
            accountId, token
        }, Request.Scheme);

        await emailSender.SendEmailAsync(studentEmail, "Подтверждение почты", $"""
                                                                               <head></head>
                                                                               <body>
                                                                               <a href="{confirmationUrl}">
                                                                               Подтвердить почту</a>
                                                                               </body>
                                                                               """);

        return RedirectToAction("Student", "Cabinet");
    }
}