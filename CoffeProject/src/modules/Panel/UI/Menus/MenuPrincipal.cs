using CoffeProject.modules.Panel.UI.Menus;
using CoffeProject.modules.Panel.Application.Interfaces;
using System;

namespace CoffeProject.modules.Panel.UI.Menus
{
    public class MenuPrincipal(IPanelService panelService)
    {
        private readonly IPanelService _panelService = panelService;

        public void Mostrar(int usuarioId)
        {
            if (_panelService.VerificarRol(usuarioId, "Administrador"))
            {
                new MenuAdmin().Mostrar();
            }
            else if (_panelService.VerificarRol(usuarioId, "Cliente"))
            {
                new MenuCliente().Mostrar();
            }
            else if (_panelService.VerificarRol(usuarioId, "Vendedor"))
            {
                new MenuVendedor().Mostrar();
            }
            else
            {
                Console.WriteLine("Rol no reconocido o sin permisos asignados.");
            }
        }
    }
}
