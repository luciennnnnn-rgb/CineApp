using Npgsql;
using CineApp.Models;

namespace CineApp.Services;

public class GenreService
{
    private readonly NpgsqlConnection _connection;

    public GenreService(NpgsqlConnection connection)
    {
        _connection = connection;
    }

    public List<Genre> GetAll()
    {
        _connection.Open();
        var command = _connection.CreateCommand();
        command.CommandText = "SELECT Id, Nom FROM Genre";

        using var reader = command.ExecuteReader();
        var genres = new List<Genre>();

        while (reader.Read())
        {
            genres.Add(new Genre
            {
                Id = reader.GetInt32(0),
                Nom = reader.GetString(1)
            });
        }

        _connection.Close();
        return genres;
    }
}