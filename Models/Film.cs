namespace CineApp.Models;

public class Film
{
    public int Id { get; set; }
    public string Titre { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int AnneeSortie { get; set; }
    public int GenreId { get; set; }
    public string? GenreNom { get; set; }
}