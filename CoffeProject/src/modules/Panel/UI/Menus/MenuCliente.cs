using System;
using CoffeProject.modules.VariedadesCafe.Application.Services;
using CoffeProject.modules.VariedadesCafe.UI;

namespace CoffeProject.modules.Panel.UI.Menus
{
    public class MenuCliente
    {
        private readonly VariedadService _variedadService;

        public MenuCliente(VariedadService variedadService)
        {
            _variedadService = variedadService;
        }

        public void Mostrar()
        {
            bool salir = false;

            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("╔═══════════════════════════════════════════════════════╗");
                Console.WriteLine("║                 👤 MENÚ DEL CLIENTE                   ║");
                Console.WriteLine("╚═══════════════════════════════════════════════════════╝\n");

                Console.WriteLine("1️⃣  Ver catálogo de variedades de café");
                Console.WriteLine("0️⃣  Volver al panel principal\n");

                Console.Write("👉 Seleccione una opción: ");
                string? opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        new CatalogoMenu(_variedadService).Mostrar();
                        break;
                    case "0":
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("\n⚠️ Opción no válida. Presione una tecla...");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
