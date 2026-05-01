using CineApp.Models;
using CineApp.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CineApp.Pages;

public class IndexModel : PageModel
{
    private readonly FilmService _filmService;
    public List<Film> Films { get; set; } = new();

    public IndexModel(FilmService filmService)
    {
        _filmService = filmService;
    }

    public void OnGet()
    {
        Films = _filmService.GetAll();
    }
}