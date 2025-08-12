using CoffeProject.modules.User.Application.Interfaces;
using System;

namespace CoffeProject.modules.Panel.UI.Menus
{
    public class MenuPrincipal
    {
        private readonly IUserService _userService;

        public MenuPrincipal(IUserService userService)
        {
            _userService = userService;
        }

        public void Mostrar(int usuarioId)
        {
            if (_userService.VerificarRol(usuarioId, "admin"))
            {
                MostrarMenuAdmin();
            }
            else if (_userService.VerificarRol(usuarioId, "editor"))
            {
                MostrarMenuEditor();
            }
            else
            {
                MostrarMenuViewer();
            }
        }

        private void MostrarMenuAdmin()
        {
            Console.WriteLine("=== Menú Administrador ===");
            // Opciones de administración
        }

        private void MostrarMenuEditor()
        {
            Console.WriteLine("=== Menú Editor ===");
            // Opciones de edición
        }

        private void MostrarMenuViewer()
        {
            Console.WriteLine("=== Menú Usuario ===");
            // Opciones de solo lectura
        }
    }
}
