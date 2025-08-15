using System;
using CoffeProject.modules.Panel.Application.Interfaces;

namespace CoffeProject.modules.Panel.UI.Menus
{
    public class MenuAdmin
    {
        private readonly IAdmin _adminService;

        public MenuAdmin(IAdmin adminService)
        {
            _adminService = adminService;
        }

        public void Mostrar()
        {
            bool salir = false;
            while (!salir)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("╔══════════════════════════════════════╗");
                Console.WriteLine("║       🛠  MENÚ ADMINISTRADOR          ║");
                Console.WriteLine("╚══════════════════════════════════════╝");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("1. ➕ Crear usuario");
                Console.WriteLine("2. ✏️  Editar usuario");
                Console.WriteLine("3. 🗑  Eliminar usuario");
                Console.WriteLine("4. 📜 Listar usuarios");
                Console.WriteLine("5. 🚪 Cerrar sesión");
                Console.ResetColor();

                Console.Write("\nSeleccione una opción: ");
                var opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        CrearUsuario();
                        break;
                    case "2":
                        EditarUsuario();
                        break;
                    case "3":
                        EliminarUsuario();
                        break;
                    case "4":
                        ListarUsuarios();
                        break;
                    case "5":
                        salir = true;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("❌ Opción inválida");
                        Console.ResetColor();
                        break;
                }

                if (!salir)
                {
                    Console.WriteLine("\nPresione una tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }

        private void CrearUsuario()
            {
                Console.Clear();
                Console.WriteLine("=== ➕ CREAR USUARIO ===\n");

                Console.Write("Nombre de usuario: ");
                string nombre = Console.ReadLine() ?? "";
                Console.Write("Correo: ");
                string correo = Console.ReadLine() ?? "";
                Console.Write("Contraseña: ");
                string contrasena = Console.ReadLine() ?? "";

                Console.WriteLine("\n📋 Roles disponibles:");
                foreach (var rol in _adminService.ListarRoles())
                    Console.WriteLine($"{rol.Id}. {rol.Nombre}");

                Console.Write("ID del rol: ");
                if (!int.TryParse(Console.ReadLine(), out int roleId))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n❌ ID de rol inválido.");
                    Console.ResetColor();
                    return;
                }

                try
                {
                    _adminService.CrearUsuario(nombre, correo, contrasena, roleId);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n✅ Usuario creado correctamente.");
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\n❌ Error: {ex.Message}");
                }
                finally
                {
                    Console.ResetColor();
                }
            }


        private void EditarUsuario()
        {
            Console.Clear();
            Console.WriteLine("=== ✏️ EDITAR USUARIO ===\n");

            Console.Write("Buscar por (1) Nombre o (2) Correo: ");
            var tipoBusqueda = Console.ReadLine();
            var usuario = tipoBusqueda == "1"
                ? _adminService.BuscarPorNombre(Console.ReadLine() ?? "")
                : _adminService.BuscarPorCorreo(Console.ReadLine() ?? "");

            if (usuario == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n❌ Usuario no encontrado.");
                Console.ResetColor();
                return;
            }

            Console.Write("Nuevo nombre (enter para mantener): ");
            string nuevoNombre = Console.ReadLine();
            Console.Write("Nuevo correo (enter para mantener): ");
            string nuevoCorreo = Console.ReadLine();
            Console.Write("Nueva contraseña (enter para mantener): ");
            string nuevaContrasena = Console.ReadLine();

            Console.WriteLine("\n📋 Roles disponibles:");
            foreach (var rol in _adminService.ListarRoles())
                Console.WriteLine($"{rol.Id}. {rol.Nombre}");

            Console.Write("Nuevo ID de rol (enter para mantener): ");
            string rolInput = Console.ReadLine();
            int? nuevoRoleId = string.IsNullOrEmpty(rolInput) ? null : int.Parse(rolInput);

            Console.Write("¿Activo? (s/n, enter para mantener): ");
            string activoInput = Console.ReadLine();
            bool? estaActivo = activoInput?.ToLower() == "s" ? true :
                               activoInput?.ToLower() == "n" ? false : null;

            _adminService.EditarUsuario(usuario.Id, nuevoNombre, nuevoCorreo, nuevaContrasena, nuevoRoleId, estaActivo);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n✅ Usuario actualizado.");
            Console.ResetColor();
        }

        private void EliminarUsuario()
        {
            Console.Clear();
            Console.WriteLine("=== 🗑 ELIMINAR USUARIO ===\n");

            Console.Write("ID del usuario a eliminar: ");
            int id = int.Parse(Console.ReadLine() ?? "0");

            _adminService.EliminarUsuario(id);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n✅ Usuario eliminado.");
            Console.ResetColor();
        }

        private void ListarUsuarios()
        {
            Console.Clear();
            Console.WriteLine("=== 📜 LISTA DE USUARIOS ===\n");

            foreach (var u in _adminService.ListarUsuarios())
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"ID: {u.Id}");
                Console.ResetColor();
                Console.WriteLine($"👤 Usuario: {u.NombreUsuario}");
                Console.WriteLine($"📧 Correo: {u.Correo}");
                Console.WriteLine($"🔑 Rol: {u.Rol.Nombre}");
                Console.WriteLine($"📌 Activo: {(u.EstaActivo ? "Sí" : "No")}");
                Console.WriteLine(new string('-', 35));
            }
        }
    }
}
