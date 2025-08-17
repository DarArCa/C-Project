using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using CoffeProject.shared.Context;
using CoffeProject.modules.Panel.Infrastructure.Repositories;
using CoffeProject.modules.Panel.Application.Services;
using CoffeProject.modules.Panel.Application.Interfaces;
using CoffeProject.modules.Panel.UI.Menus;
using CoffeProject.modules.User.Infrastructure.Repositories;
using CoffeProject.modules.User.Application.Services;
using CoffeProject.modules.User.UI.Menus;
using CoffeProject.modules.VariedadesCafe.Infrastructure.Repositories;
using CoffeProject.modules.VariedadesCafe.Application.Services;

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
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("❌ No hay cadena de conexión 'MySqlDB' en appsettings.json");
            Console.ResetColor();
            return;
        }

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseMySql(conn, new MySqlServerVersion(new Version(8, 0, 21)));

        using var context = new AppDbContext(optionsBuilder.Options);

        // Servicios panel y admin
        IPanelRepository panelRepo = new PanelRepository(context);
        IPanelService panelService = new PanelService(panelRepo);

        IAdminRepository adminRepo = new AdminRepository(context);
        IAdmin adminService = new AdminService(adminRepo);

        // Servicio de variedades (usa repo directo con conexión MySQL)
        var variedadRepo = new VariedadRepository(conn);
        var variedadService = new VariedadService(variedadRepo);

        bool running = true;
        while (running)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔═══════════════════════════════════════════╗");
            Console.WriteLine("║        ☕ BIENVENIDO A COFFE PROJECT      ║");
            Console.WriteLine("╚═══════════════════════════════════════════╝");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("1️⃣  Registrarse");
            Console.WriteLine("2️⃣  Iniciar Sesión");
            Console.WriteLine("3️⃣  Salir");
            Console.ResetColor();

            Console.Write("\nSeleccione una opción: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    var userRepo = new UserRepository(context);
                    var userService = new UserService(userRepo);
                    var registerMenu = new RegisterMenu(userService);
                    registerMenu.Mostrar();
                    break;

                case "2":
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("=== 🔑 INICIO DE SESIÓN ===");
                    Console.ResetColor();

                    Console.Write("👤 Usuario: ");
                    var username = Console.ReadLine() ?? string.Empty;
                    Console.Write("🔒 Contraseña: ");
                    var password = Console.ReadLine() ?? string.Empty;

                    var usuarioId = panelService.LoginYObtenerId(username, password);

                    if (usuarioId == null)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n❌ Usuario o contraseña incorrectos.");
                        Console.ResetColor();
                        Console.WriteLine("\nPresione una tecla para continuar...");
                        Console.ReadKey();
                    }
                    else
                    {
                        var menu = new MenuPrincipal(panelService, adminService, variedadService);
                        menu.Mostrar(usuarioId.Value);
                    }
                    break;

                case "3":
                    running = false;
                    break;

                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n❌ Opción inválida, intente nuevamente.");
                    Console.ResetColor();
                    break;
            }
        }

        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("👋 Gracias por usar Coffe Project. ¡Hasta pronto!");
        Console.ResetColor();
    }
}
