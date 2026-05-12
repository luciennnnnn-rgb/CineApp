using CineApp.Models;
using CineApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CineApp.Pages;

public class DetailsModel : PageModel
{
    private readonly FilmService _filmService;
    public Film? Film { get; set; }

    public DetailsModel(FilmService filmService)
    {
        _filmService = filmService;
    }

    public IActionResult OnGet(int id)
    {
        Film = _filmService.GetById(id);
        if (Film is null) return NotFound();
        return Page();
    }
}