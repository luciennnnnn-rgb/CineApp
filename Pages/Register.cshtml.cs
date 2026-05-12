using CineApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CineApp.Pages;

public class RegisterModel : PageModel
{
    private readonly UserService _userService;

    [BindProperty]
    public string Email { get; set; } = string.Empty;

    [BindProperty]
    public string Password { get; set; } = string.Empty;

    public string? Erreur { get; set; }

    public RegisterModel(UserService userService)
    {
        _userService = userService;
    }

    public void OnGet() { }

    public IActionResult OnPost()
    {
        try
        {
            _userService.Register(Email, Password);
            return RedirectToPage("/Login");
        }
        catch
        {
            Erreur = "Cet email est déjà utilisé.";
            return Page();
        }
    }
}