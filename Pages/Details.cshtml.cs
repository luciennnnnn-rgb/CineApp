using CineApp.Models;
using CineApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace CineApp.Pages;

public class DetailsModel : PageModel
{
    private readonly FilmService _filmService;
    private readonly CommentaireService _commentaireService;

    public Film? Film { get; set; }
    public List<Commentaire> Commentaires { get; set; } = new();

    public DetailsModel(FilmService filmService, CommentaireService commentaireService)
    {
        _filmService = filmService;
        _commentaireService = commentaireService;
    }

    public IActionResult OnGet(int id)
    {
        Film = _filmService.GetById(id);
        if (Film is null) return NotFound();
        Commentaires = _commentaireService.GetByFilm(id);
        return Page();
    }

    public IActionResult OnPost(int filmId, string contenu, int note)
    {
        var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdStr is null) return RedirectToPage("/Login");

        var commentaire = new Commentaire
        {
            Contenu = contenu,
            Note = note,
            DatePublication = DateTime.Now,
            FilmId = filmId,
            UserId = int.Parse(userIdStr)
        };

        _commentaireService.Create(commentaire);
        return RedirectToPage(new { id = filmId });
    }
}