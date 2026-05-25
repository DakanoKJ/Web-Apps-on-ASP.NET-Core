using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalAccount.Models;
using PersonalAccount.Services.Auth;
using PersonalAccount.ViewModels;

namespace PersonalAccount.Controllers;

public class AccountController(IAuthService auth) : Controller
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
        var account = await auth.ValidateCredentialsAsync(login.Email, login.Password);
        if (account is null)
        {
            ModelState.AddModelError(string.Empty, "Invalid login attempt");
            return View(login);
        }

        await auth.SignInAsync(HttpContext, account);
        return Redirect(login.ReturnUrl ?? "/");
    }

    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await auth.SignOutAsync(HttpContext);
        return RedirectToAction("Index", "Home");
    }
}