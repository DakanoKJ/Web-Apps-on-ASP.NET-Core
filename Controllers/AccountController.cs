using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalAccount.Services.Account;
using PersonalAccount.ViewModels;

namespace PersonalAccount.Controllers;

public class AccountController(IAccountService accountService) : Controller
{
    // method GET
    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        return View(new LoginViewModel
        {
            ReturnUrl = returnUrl
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel login)
    {
        if (!ModelState.IsValid) return View(login);
        var account = await accountService.ValidateCredentialsAsync(login.Email, login.Password);
        if (account is null)
        {
            ModelState.AddModelError(string.Empty, "Invalid login attempt");
            return View(login);
        }

        await accountService.SignInAsync(HttpContext, account);
        return Redirect(login.ReturnUrl ?? "/");
    }

    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await accountService.SignOutAsync(HttpContext);
        return RedirectToAction("Index", "Home");
    }
}