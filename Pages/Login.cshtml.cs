using CineApp.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace CineApp.Pages;

public class LoginModel : PageModel
{
    private readonly UserService _userService;

    [BindProperty]
    public string Email { get; set; } = string.Empty;

    [BindProperty]
    public string Password { get; set; } = string.Empty;

    public string? Erreur { get; set; }

    public LoginModel(UserService userService)
    {
        _userService = userService;
    }

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        var user = _userService.Login(Email, Password);

        if (user is null)
        {
            Erreur = "Email ou mot de passe incorrect.";
            return Page();
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Prenom + " " + user.Nom),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role),
        };
        var identity = new ClaimsIdentity(claims, "Cookies");
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync("Cookies", principal);

        return RedirectToPage("/Index");
    }
}