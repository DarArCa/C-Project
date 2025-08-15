using System;
using CoffeProject.modules.User.Application.Interfaces;
using CoffeProject.modules.User.Domain.Entities;

namespace CoffeProject.modules.User.UI.Menus
{
    public class RegisterMenu
    {
        private readonly IUserService _userService;

        public RegisterMenu(IUserService userService)
        {
            _userService = userService;
        }

        public void Mostrar()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("=== 📝 REGISTRO DE CLIENTE ===");
            Console.ResetColor();

            Console.Write("Nombre de usuario: ");
            string nombre = Console.ReadLine() ?? "";
            Console.Write("Correo: ");
            string correo = Console.ReadLine() ?? "";
            Console.Write("Contraseña: ");
            string contrasena = Console.ReadLine() ?? "";

            try
            {
                _userService.Registrar(new UserRegisterDto
                {
                    NombreUsuario = nombre,
                    Correo = correo,
                    ContrasenaHash = contrasena
                });

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n✅ Registro completado. ¡Ya puedes iniciar sesión!");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n❌ Error: {ex.Message}");
            }
            finally
            {
                Console.ResetColor();
                Console.WriteLine("\nPresione una tecla para continuar...");
                Console.ReadKey();
            }
        }
    }
}
