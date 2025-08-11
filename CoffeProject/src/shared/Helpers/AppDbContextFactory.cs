using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using CoffeProject.shared.Context;

namespace CoffeProject.shared.Helpers
{
    public class AppDbContextFactory
    {
        public static AppDbContext Create()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var connectionString = configuration.GetConnectionString("MySqlDB");
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException("No se encontró la cadena de conexión 'MySqlDB' en appsettings.json");

            Version detectedVersion;
            try
            {
                detectedVersion = MySqlVersionResolver.DetectVersion(connectionString);
            }
            catch
            {
                detectedVersion = new Version(8, 0, 21);
            }

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseMySql(connectionString, new MySqlServerVersion(detectedVersion))
                .Options;

            return new AppDbContext(options);
        }
    }
}
