using CoffeProject.modules.Panel.Application.Interfaces;
using System;

namespace CoffeProject.modules.Panel.UI.Menus
{
    public class MenuPrincipal
    {
        private readonly IPanelService _panelService;
        private readonly IAdmin _adminService;

        public MenuPrincipal(IPanelService panelService, IAdmin adminService)
        {
            _panelService = panelService;
            _adminService = adminService;
        }

        public void Mostrar(int usuarioId)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║         📋 PANEL PRINCIPAL           ║");
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.ResetColor();

            if (_panelService.VerificarRol(usuarioId, "admin"))
            {
                Console.WriteLine("\n🔑 Acceso: Administrador");
                Console.WriteLine("Cargando menú de administración...");
                new MenuAdmin(_adminService).Mostrar();
            }
            else if (_panelService.VerificarRol(usuarioId, "Cliente"))
            {
                Console.WriteLine("\n👤 Acceso: Cliente");
                Console.WriteLine("Cargando menú de cliente...");
                new MenuCliente().Mostrar();
            }
            else if (_panelService.VerificarRol(usuarioId, "Vendedor"))
            {
                Console.WriteLine("\n🛒 Acceso: Vendedor");
                Console.WriteLine("Cargando menú de vendedor...");
                new MenuVendedor().Mostrar();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n❌ Rol no reconocido o sin permisos asignados.");
                Console.ResetColor();
            }
        }
    }
}
