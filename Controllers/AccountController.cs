using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalAccount.Models;
using PersonalAccount.Services.Auth;

namespace PersonalAccount.Controllers;

public class AccountController(IStudentAuthService auth) : Controller
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
        var student = await auth.ValidateStudentAsync(login.Email, login.Password);
        if (student is null)
        {
            ModelState.AddModelError(string.Empty, "Invalid login attempt");
            return View(login);
        }

        await auth.SignInAsync(HttpContext, student);
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