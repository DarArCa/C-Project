using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using CoffeProject.shared.Context;
using CoffeProject.modules.Panel.Infrastructure.Repositories;
using CoffeProject.modules.Panel.Application.Services;
using CoffeProject.modules.Panel.UI.Menus;
using CoffeProject.src.modules.Users.Infrastructure.Repositories;
using CoffeProject.src.modules.Users.Application;


internal class Program
{
    private static async Task Main(string[] args)
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
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("╔════════════════════════════════════╗");
                Console.WriteLine("║     ✨  MENÚ PRINCIPAL  ✨        ║");
                Console.WriteLine("╠════════════════════════════════════╣");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("║ 1 ─ Registrarse                    ║");
                Console.WriteLine("║ 2 ─ Iniciar Sesión                 ║");
                Console.WriteLine("║ 3 ─ Salir                          ║");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("╚════════════════════════════════════╝");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("\n➤ Seleccione una opción (1-3): ");
                Console.ResetColor();
                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("=== Registro de nuevo usuario ===\n");
                        Console.ResetColor();

                        Console.Write("Nombre de usuario: ");
                        var nuevoUsername = Console.ReadLine() ?? string.Empty;

                        Console.Write("Correo electrónico: ");
                        var nuevoCorreo = Console.ReadLine() ?? string.Empty;

                        Console.Write("Contraseña: ");
                        var nuevoPassword = Console.ReadLine() ?? string.Empty;

                        try
                        {
                            var userRepo = new UserRepository(context);
                            var userService = new UserService(userRepo);

                            var nuevoUsuario = await userService.CrearUsuarioAsync(
                                nuevoUsername,
                                nuevoCorreo,
                                nuevoPassword
                            );

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"\n✅ Usuario '{nuevoUsuario.NombreUsuario}' registrado como Viewer (RoleId={nuevoUsuario.RoleId}).");
                            Console.ResetColor();
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"\n❌ Error al registrar usuario: {ex.Message}");
                            Console.ResetColor();
                        }
                        Console.WriteLine("\nPresione cualquier tecla para continuar...");
                        Console.ReadKey();
                        break;

                    case "2":
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("=== Iniciar Sesión ===\n");
                        Console.ResetColor();

                        Console.Write("Usuario: ");
                        var username = Console.ReadLine() ?? string.Empty;

                        Console.Write("Contraseña: ");
                        var password = Console.ReadLine() ?? string.Empty;

                        var usuarioId = panelService.LoginYObtenerId(username, password);

                        if (usuarioId == null)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\n⚠ Usuario o contraseña incorrectos.");
                            Console.ResetColor();
                            Console.WriteLine("\nPresione cualquier tecla para continuar...");
                            Console.ReadKey();
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
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n⚠ Opción inválida, intente nuevamente.");
                        Console.ResetColor();
                        Console.WriteLine("\nPresione cualquier tecla para continuar...");
                        Console.ReadKey();
                        break;
                }
            }
    }
}
