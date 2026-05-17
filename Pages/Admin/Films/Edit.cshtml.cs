using CineApp.Models;
using CineApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CineApp.Pages.Admin.Films;

[Authorize(Roles = "admin")]
public class EditModel : PageModel
{
    private readonly FilmService _filmService;
    private readonly GenreService _genreService;

    [BindProperty]
    public Film Film { get; set; } = new();
    public List<Genre> Genres { get; set; } = new();

    public EditModel(FilmService filmService, GenreService genreService)
    {
        _filmService = filmService;
        _genreService = genreService;
    }

    public IActionResult OnGet(int id)
    {
        Film = _filmService.GetById(id)!;
        Genres = _genreService.GetAll();
        if (Film is null) return NotFound();
        return Page();
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            Genres = _genreService.GetAll();
            return Page();
        }
        _filmService.Update(Film);
        return RedirectToPage("/Admin/Films/Index");
    }
}