using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using CoffeProject.shared.Context;
using CoffeProject.modules.Panel.Infrastructure.Repositories;
using CoffeProject.modules.Panel.Application.Services;
using CoffeProject.modules.Panel.UI.Menus;

internal class Program
{
    private static void Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var conn = configuration.GetConnectionString("MySqlDB");
        if (string.IsNullOrWhiteSpace(conn))
        {
            Console.WriteLine("No hay cadena de conexión 'MySqlDB' en appsettings.json");
            return;
        }

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseMySql(conn, new MySqlServerVersion(new Version(8, 0, 21)));

        using var context = new AppDbContext(optionsBuilder.Options);
        var panelRepo = new PanelRepository(context);
        var panelService = new PanelService(panelRepo);

        bool running = true;
        while (running)
        {
            Console.WriteLine("1---Registrarse");
            Console.WriteLine("2---Iniciar Sesion");
            Console.WriteLine("3---Salir");
            Console.Write("Seleccione una opción: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    Console.WriteLine("Funcionalidad de registro aún no implementada.");
                    break;

                case "2":
                    Console.Clear();
                    Console.Write("Usuario: ");
                    var username = Console.ReadLine() ?? string.Empty;
                    Console.Write("Contraseña: ");
                    var password = Console.ReadLine() ?? string.Empty;

                    var usuarioId = panelService.LoginYObtenerId(username, password);

                    if (usuarioId == null)
                    {
                        Console.WriteLine("Usuario o contraseña incorrectos.");
                    }
                    else
                    {
                        var menu = new MenuPrincipal(panelService);
                        menu.Mostrar(usuarioId.Value);
                        running = false;
                    }
                    break;

                case "3":
                    running = false;
                    break;

                default:
                    Console.WriteLine("Opción inválida, intente nuevamente.");
                    break;
            }
        }
    }
}
