using System.ComponentModel.DataAnnotations;

namespace CineApp.Models;

public class Film
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Le titre est obligatoire.")]
    [StringLength(100, ErrorMessage = "Le titre ne peut pas dépasser 100 caractères.")]
    public string Titre { get; set; } = string.Empty;

    [Required(ErrorMessage = "La description est obligatoire.")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "L'année de sortie est obligatoire.")]
    [Range(1888, 2100, ErrorMessage = "L'année doit être entre 1888 et 2100.")]
    public int AnneeSortie { get; set; }

    public int GenreId { get; set; }
    public string? GenreNom { get; set; }
}