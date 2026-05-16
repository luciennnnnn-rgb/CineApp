using CineApp.Models;
using CineApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CineApp.Pages.Admin.Films;

[Authorize(Roles = "admin")]
public class CreateModel : PageModel
{
    private readonly FilmService _filmService;
    private readonly GenreService _genreService;

    [BindProperty]
    public Film Film { get; set; } = new();
    public List<Genre> Genres { get; set; } = new();

    public CreateModel(FilmService filmService, GenreService genreService)
    {
        _filmService = filmService;
        _genreService = genreService;
    }

    public void OnGet()
    {
        Genres = _genreService.GetAll();
    }

    public IActionResult OnPost()
    {
        _filmService.Create(Film);
        return RedirectToPage("/Admin/Films/Index");
    }
}