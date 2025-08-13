using System;

namespace CoffeProject.modules.Panel.UI.Menus
{
    public class MenuCliente
    {
        public void Mostrar()
        {
            Console.WriteLine("=== Menú Cliente ===");
            Console.WriteLine("1. Ver productos");
            Console.WriteLine("2. Realizar pedido");
            Console.WriteLine("3. Ver historial de compras");
            Console.WriteLine("4. Salir");

            Console.Write("Seleccione una opción: ");
            var opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    Console.WriteLine("Mostrando productos...");
                    break;
                case "2":
                    Console.WriteLine("Realizando pedido...");
                    break;
                case "3":
                    Console.WriteLine("Historial de compras...");
                    break;
                case "4":
                    Console.WriteLine("Saliendo...");
                    break;
                default:
                    Console.WriteLine("Opción inválida.");
                    break;
            }
        }
    }
}
