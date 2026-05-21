using System.ComponentModel.DataAnnotations;

namespace CineApp.Models;

public class User
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Le prénom est obligatoire.")]
    public string Prenom { get; set; } = string.Empty;

    [Required(ErrorMessage = "Le nom est obligatoire.")]
    public string Nom { get; set; } = string.Empty;

    [Required(ErrorMessage = "L'email est obligatoire.")]
    [EmailAddress(ErrorMessage = "L'email n'est pas valide.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Le mot de passe est obligatoire.")]
    [MinLength(6, ErrorMessage = "Le mot de passe doit faire au moins 6 caractères.")]
    public string Password { get; set; } = string.Empty;

    public string Role { get; set; } = "user";
}