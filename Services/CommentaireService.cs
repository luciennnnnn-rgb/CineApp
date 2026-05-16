using Npgsql;
using CineApp.Models;

namespace CineApp.Services;

public class CommentaireService
{
    private readonly NpgsqlConnection _connection;

    public CommentaireService(NpgsqlConnection connection)
    {
        _connection = connection;
    }

    public List<Commentaire> GetByFilm(int filmId)
    {
        _connection.Open();
        var command = _connection.CreateCommand();
        command.CommandText = @"
            SELECT c.Id, c.Contenu, c.Note, c.DatePublication, c.FilmId, c.UserId, u.Email
            FROM Commentaire c
            JOIN Users u ON c.UserId = u.Id
            WHERE c.FilmId = @filmId
            ORDER BY c.DatePublication DESC";

        var param = command.CreateParameter();
        param.ParameterName = "@filmId";
        param.Value = filmId;
        command.Parameters.Add(param);

        using var reader = command.ExecuteReader();
        var commentaires = new List<Commentaire>();

        while (reader.Read())
        {
            commentaires.Add(new Commentaire
            {
                Id = reader.GetInt32(0),
                Contenu = reader.GetString(1),
                Note = reader.GetInt32(2),
                DatePublication = reader.GetDateTime(3),
                FilmId = reader.GetInt32(4),
                UserId = reader.GetInt32(5),
                UserEmail = reader.GetString(6)
            });
        }

        _connection.Close();
        return commentaires;
    }

    public void Create(Commentaire commentaire)
    {
        _connection.Open();
        var command = _connection.CreateCommand();
        command.CommandText = @"
            INSERT INTO Commentaire (Contenu, Note, DatePublication, FilmId, UserId)
            VALUES (@contenu, @note, @date, @filmId, @userId)";

        var p1 = command.CreateParameter();
        p1.ParameterName = "@contenu";
        p1.Value = commentaire.Contenu;
        command.Parameters.Add(p1);

        var p2 = command.CreateParameter();
        p2.ParameterName = "@note";
        p2.Value = commentaire.Note;
        command.Parameters.Add(p2);

        var p3 = command.CreateParameter();
        p3.ParameterName = "@date";
        p3.Value = commentaire.DatePublication;
        command.Parameters.Add(p3);

        var p4 = command.CreateParameter();
        p4.ParameterName = "@filmId";
        p4.Value = commentaire.FilmId;
        command.Parameters.Add(p4);

        var p5 = command.CreateParameter();
        p5.ParameterName = "@userId";
        p5.Value = commentaire.UserId;
        command.Parameters.Add(p5);

        command.ExecuteNonQuery();
        _connection.Close();
    }
}