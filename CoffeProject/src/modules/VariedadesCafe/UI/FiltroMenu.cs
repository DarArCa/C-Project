using System;
using System.Collections.Generic;
using CoffeProject.modules.VariedadesCafe.Application.Interfaces;
using CoffeProject.modules.VariedadesCafe.Domain.Entities;

namespace CoffeProject.modules.VariedadesCafe.UI
{
    public class FiltroMenu
    {
        private readonly IVariedadService _service;

        public FiltroMenu(IVariedadService service)
        {
            _service = service;
        }

        public void Mostrar()
        {
            bool salir = false;
            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("╔════════════════════════════════════╗");
                Console.WriteLine("║       🔎 BÚSQUEDA AVANZADA        ║");
                Console.WriteLine("╚════════════════════════════════════╝\n");

                Console.WriteLine("1) Filtrar por Porte (Alto/Medio/Bajo)");
                Console.WriteLine("2) Filtrar por Tamaño de grano (Pequeño/Medio/Grande)");
                Console.WriteLine("3) Filtrar por Altitud (rango)");
                Console.WriteLine("4) Filtrar por Potencial de rendimiento");
                Console.WriteLine("5) Filtrar por Nivel de calidad (1-5)");
                Console.WriteLine("6) Filtrar por Resistencia (ej: Roya = Resistente)");
                Console.WriteLine("7) Filtrar por Etiqueta");
                Console.WriteLine("0) Volver\n");

                Console.Write("👉 Opción: ");
                var op = Console.ReadLine();

                switch (op)
                {
                    case "1":
                        Console.Write("Ingrese porte (Alto/Medio/Bajo): ");
                        MostrarListado(_service.FiltrarPorPorte(Console.ReadLine() ?? ""));
                        break;
                    case "2":
                        Console.Write("Ingrese tamaño (Pequeño/Medio/Grande): ");
                        MostrarListado(_service.FiltrarPorTamano(Console.ReadLine() ?? ""));
                        break;
                    case "3":
                        Console.Write("Altitud mínima: ");
                        int.TryParse(Console.ReadLine(), out int min);
                        Console.Write("Altitud máxima: ");
                        int.TryParse(Console.ReadLine(), out int max);
                        MostrarListado(_service.FiltrarPorAltitud(min, max));
                        break;
                    case "4":
                        Console.Write("Potencial (Muy bajo/Bajo/Medio/Alto/Excepcional): ");
                        MostrarListado(_service.FiltrarPorRendimiento(Console.ReadLine() ?? ""));
                        break;
                    case "5":
                        Console.Write("Nivel de calidad (1-5): ");
                        if (int.TryParse(Console.ReadLine(), out int nivel)) MostrarListado(_service.FiltrarPorCalidad(nivel));
                        else { Console.WriteLine("Entrada inválida"); Console.ReadKey(); }
                        break;
                    case "6":
                        Console.Write("Tipo de resistencia (Roya/Antracnosis/Nematodos): ");
                        var tipo = Console.ReadLine() ?? "";
                        Console.Write("Nivel (Susceptible/Tolerante/Resistente): ");
                        var nivelRes = Console.ReadLine() ?? "";
                        MostrarListado(_service.FiltrarPorResistencia(tipo, nivelRes));
                        break;
                    case "7":
                        Console.Write("Etiqueta (ej: 'Cafe de especialidad'): ");
                        MostrarListado(_service.FiltrarPorEtiqueta(Console.ReadLine() ?? ""));
                        break;
                    case "0":
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("Opción inválida"); Console.ReadKey();
                        break;
                }
            }
        }

        private void MostrarListado(List<Variedad> l)
        {
            Console.Clear();
            if (l == null || l.Count == 0)
            {
                Console.WriteLine("No se encontraron resultados.");
                Console.ReadKey();
                return;
            }

            foreach (var v in l)
            {
                Console.WriteLine($"{v.Id} | {v.NombreComun} | {v.NombreCientifico} | Porte: {v.Porte} | Tamaño: {v.TamanoGrano} | Alt: {v.AltitudOptimaM} msnm");
            }

            Console.WriteLine("\nIngrese ID para ver ficha completa o ENTER para volver:");
            var s = Console.ReadLine();
            if (int.TryParse(s, out int id))
            {
                var ficha = _service.ObtenerFicha(id);
                if (ficha != null)
                {
                    // reutiliza la ficha del catálogo
                    new CatalogoMenu(_service).Mostrar(); // si quieres abrir el catálogo de nuevo (simple)
                    // o podrías mostrar la ficha directamente: (pero CatalogoMenu tiene método privado MostrarFicha)
                }
            }
        }
    }
}
