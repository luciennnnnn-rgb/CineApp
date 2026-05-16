using CineApp.Models;
using CineApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CineApp.Pages;

public class IndexModel : PageModel
{
    private readonly FilmService _filmService;
    private readonly GenreService _genreService;

    public List<Film> Films { get; set; } = new();
    public List<Genre> Genres { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public int? GenreId { get; set; }

    public IndexModel(FilmService filmService, GenreService genreService)
    {
        _filmService = filmService;
        _genreService = genreService;
    }

    public void OnGet()
    {
        Genres = _genreService.GetAll();
        Films = GenreId.HasValue
            ? _filmService.GetByGenre(GenreId.Value)
            : _filmService.GetAll();
    }
}