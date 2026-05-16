namespace CineApp.Models;

public class Commentaire
{
    public int Id { get; set; }
    public string Contenu { get; set; } = string.Empty;
    public int Note { get; set; }
    public DateTime DatePublication { get; set; }
    public int FilmId { get; set; }
    public int UserId { get; set; }
    public string? UserEmail { get; set; }
}