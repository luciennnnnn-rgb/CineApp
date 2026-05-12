using Npgsql;
using CineApp.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace CineApp.Services;

public class UserService
{
    private readonly NpgsqlConnection _connection;

    public UserService(NpgsqlConnection connection)
    {
        _connection = connection;
    }

    private string HashPassword(string password, byte[] salt)
    {
        return Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));
    }

    public void Register(string email, string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);
        string hashed = HashPassword(password, salt);
        string saltStr = Convert.ToBase64String(salt);

        _connection.Open();
        var command = _connection.CreateCommand();
        command.CommandText = @"
            INSERT INTO Users (Email, Password, Role)
            VALUES (@email, @password, 'user')";

        var p1 = command.CreateParameter();
        p1.ParameterName = "@email";
        p1.Value = email;
        command.Parameters.Add(p1);

        var p2 = command.CreateParameter();
        p2.ParameterName = "@password";
        p2.Value = saltStr + ":" + hashed;
        command.Parameters.Add(p2);

        command.ExecuteNonQuery();
        _connection.Close();
    }

    public User? Login(string email, string password)
    {
        _connection.Open();
        var command = _connection.CreateCommand();
        command.CommandText = "SELECT Id, Email, Password, Role FROM Users WHERE Email = @email";

        var param = command.CreateParameter();
        param.ParameterName = "@email";
        param.Value = email;
        command.Parameters.Add(param);

        using var reader = command.ExecuteReader();

        if (reader.Read())
        {
            var stored = reader.GetString(2);
            var parts = stored.Split(':');
            var salt = Convert.FromBase64String(parts[0]);
            var hashed = HashPassword(password, salt);

            if (hashed == parts[1])
            {
                var user = new User
                {
                    Id = reader.GetInt32(0),
                    Email = reader.GetString(1),
                    Role = reader.GetString(3)
                };
                _connection.Close();
                return user;
            }
        }

        _connection.Close();
        return null;
    }
}