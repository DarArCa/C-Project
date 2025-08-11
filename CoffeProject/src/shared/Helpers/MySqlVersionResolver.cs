using System;
using MySqlConnector;

namespace CoffeProject.shared.Helpers
{
    public static class MySqlVersionResolver
    {
        public static Version DetectVersion(string connectionString)
        {
            using var conn = new MySqlConnection(connectionString);
            conn.Open();
            var raw = conn.ServerVersion; // e.g. "8.0.21-..."
            var clean = raw.Split('-')[0];
            return Version.Parse(clean);
        }
    }
}
