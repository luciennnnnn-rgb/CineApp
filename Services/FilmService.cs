using Npgsql;
using CineApp.Models;

namespace CineApp.Services;

public class FilmService
{
    private readonly NpgsqlConnection _connection;

    public FilmService(NpgsqlConnection connection)
    {
        _connection = connection;
    }

    public List<Film> GetAll()
    {
        _connection.Open();
        var command = _connection.CreateCommand();
        command.CommandText = @"
            SELECT f.Id_film, f.Titre, f.Description, f.AnneeSortie, f.GenreId, g.Nom
            FROM Film f
            JOIN Genre g ON f.GenreId = g.Id";

        using var reader = command.ExecuteReader();
        var films = new List<Film>();

        while (reader.Read())
        {
            films.Add(new Film
            {
                Id = reader.GetInt32(0),
                Titre = reader.GetString(1),
                Description = reader.GetString(2),
                AnneeSortie = reader.GetInt32(3),
                GenreId = reader.GetInt32(4),
                GenreNom = reader.GetString(5)
            });
        }

        _connection.Close();
        return films;
    }

    public Film? GetById(int id)
    {
        _connection.Open();
        var command = _connection.CreateCommand();
        command.CommandText = @"
            SELECT f.Id_film, f.Titre, f.Description, f.AnneeSortie, f.GenreId, g.Nom
            FROM Film f
            JOIN Genre g ON f.GenreId = g.Id
            WHERE f.Id_film = @id";

        var param = command.CreateParameter();
        param.ParameterName = "@id";
        param.Value = id;
        command.Parameters.Add(param);

        using var reader = command.ExecuteReader();

        if (reader.Read())
        {
            var film = new Film
            {
                Id = reader.GetInt32(0),
                Titre = reader.GetString(1),
                Description = reader.GetString(2),
                AnneeSortie = reader.GetInt32(3),
                GenreId = reader.GetInt32(4),
                GenreNom = reader.GetString(5)
            };
            _connection.Close();
            return film;
        }

        _connection.Close();
        return null;
    }

    public void Create(Film film)
    {
        _connection.Open();
        var command = _connection.CreateCommand();
        command.CommandText = @"
            INSERT INTO Film (Titre, Description, AnneeSortie, GenreId)
            VALUES (@titre, @description, @annee, @genreId)";

        var p1 = command.CreateParameter();
        p1.ParameterName = "@titre";
        p1.Value = film.Titre;
        command.Parameters.Add(p1);

        var p2 = command.CreateParameter();
        p2.ParameterName = "@description";
        p2.Value = film.Description;
        command.Parameters.Add(p2);

        var p3 = command.CreateParameter();
        p3.ParameterName = "@annee";
        p3.Value = film.AnneeSortie;
        command.Parameters.Add(p3);

        var p4 = command.CreateParameter();
        p4.ParameterName = "@genreId";
        p4.Value = film.GenreId;
        command.Parameters.Add(p4);

        command.ExecuteNonQuery();
        _connection.Close();
    }

    public void Update(Film film)
    {
        _connection.Open();
        var command = _connection.CreateCommand();
        command.CommandText = @"
            UPDATE Film SET Titre = @titre, Description = @description,
            AnneeSortie = @annee, GenreId = @genreId
            WHERE Id_film = @id";

        var p1 = command.CreateParameter();
        p1.ParameterName = "@titre";
        p1.Value = film.Titre;
        command.Parameters.Add(p1);

        var p2 = command.CreateParameter();
        p2.ParameterName = "@description";
        p2.Value = film.Description;
        command.Parameters.Add(p2);

        var p3 = command.CreateParameter();
        p3.ParameterName = "@annee";
        p3.Value = film.AnneeSortie;
        command.Parameters.Add(p3);

        var p4 = command.CreateParameter();
        p4.ParameterName = "@genreId";
        p4.Value = film.GenreId;
        command.Parameters.Add(p4);

        var p5 = command.CreateParameter();
        p5.ParameterName = "@id";
        p5.Value = film.Id;
        command.Parameters.Add(p5);

        command.ExecuteNonQuery();
        _connection.Close();
    }

    public void Delete(int id)
    {
        _connection.Open();
        var command = _connection.CreateCommand();
        command.CommandText = "DELETE FROM Film WHERE Id_film = @id";

        var param = command.CreateParameter();
        param.ParameterName = "@id";
        param.Value = id;
        command.Parameters.Add(param);

        command.ExecuteNonQuery();
        _connection.Close();
    }
}