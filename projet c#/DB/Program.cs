using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Npgsql;

class Program
{
    static void Main(string[] args)
    {
        // Configuration de la lecture de appsettings.json
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        // Lire la chaîne de connexion à partir de la configuration
        var connectionString = config.GetConnectionString("DefaultConnection");

        // Se connecter à PostgreSQL et afficher les données
        using (var conn = new NpgsqlConnection(connectionString))
        {
            conn.Open();

            using (var cmd = new NpgsqlCommand("SELECT version()", conn))
            {
                var version = cmd.ExecuteScalar().ToString();
                Console.WriteLine($"PostgreSQL version: {version}");
            }
        }
    }
}
