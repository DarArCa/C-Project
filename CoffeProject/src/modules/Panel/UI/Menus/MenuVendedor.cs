using System;

namespace CoffeProject.modules.Panel.UI.Menus
{
    public class MenuVendedor
    {
        public void Mostrar()
        {
            Console.WriteLine("=== Menú Vendedor ===");
            Console.WriteLine("1. Ver ventas");
            Console.WriteLine("2. Gestionar inventario");
            Console.WriteLine("3. Generar reportes");
            Console.WriteLine("4. Salir");
        }
    }
}
