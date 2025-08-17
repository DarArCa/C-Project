using System;
using System.Linq;
using CoffeProject.modules.VariedadesCafe.Application.Services;
using CoffeProject.modules.VariedadesCafe.Domain.Entities;

namespace CoffeProject.modules.VariedadesCafe.UI
{
    public class CatalogoMenu
    {
        private readonly VariedadService _service;

        public CatalogoMenu(VariedadService service)
        {
            _service = service;
        }

        public void Mostrar()
        {
            bool salir = false;

            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("╔═══════════════════════════════════════════════════════╗");
                Console.WriteLine("║               🌱 CATÁLOGO DE VARIEDADES ☕             ║");
                Console.WriteLine("╚═══════════════════════════════════════════════════════╝\n");

                Console.WriteLine("1️⃣  Ver todas las variedades");
                Console.WriteLine("2️⃣  Buscar variedad por ID");
                Console.WriteLine("0️⃣  Volver al menú anterior\n");

                Console.Write("👉 Seleccione una opción: ");
                string? opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        MostrarTodas();
                        break;
                    case "2":
                        BuscarPorId();
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

        private void MostrarTodas()
        {
            Console.Clear();
            var variedades = _service.ObtenerCatalogo();

            Console.WriteLine("╔═══════════════════════════════════════════════════════╗");
            Console.WriteLine("║          📋 LISTADO COMPLETO DE VARIEDADES            ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════╝\n");

            foreach (var v in variedades)
            {
                Console.WriteLine($"📌 {v.Id}. {v.NombreComun} ({v.NombreCientifico})");
            }

            Console.WriteLine("\n📖 Índice de IDs disponibles:");
            Console.WriteLine(string.Join(", ", variedades.Select(v => v.Id)));

            Console.WriteLine("\nPresione cualquier tecla para volver...");
            Console.ReadKey();
        }

        private void BuscarPorId()
        {
            Console.Clear();
            Console.WriteLine("🔍 Buscar variedad por ID\n");
            Console.Write("Ingrese el ID de la variedad: ");

            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var ficha = _service.ObtenerFicha(id);
                if (ficha != null)
                {
                    MostrarFicha(ficha);
                }
                else
                {
                    Console.WriteLine("\n⚠️ No se encontró ninguna variedad con ese ID.");
                    Console.WriteLine("Presione una tecla para continuar...");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("\n⚠️ Entrada inválida. Presione una tecla...");
                Console.ReadKey();
            }
        }

        private void MostrarFicha(Variedad v)
        {
            Console.Clear();

            Console.WriteLine("╔══════════════════════════════════════════════════════╗");
            Console.WriteLine($"║ 🌱 FICHA TÉCNICA: {v.NombreComun.ToUpper()}");
            Console.WriteLine("╚══════════════════════════════════════════════════════╝\n");

            Console.WriteLine($"🔬 Nombre científico: {v.NombreCientifico}");
            Console.WriteLine($"🖼 Imagen: {v.ImagenReferencia}");
            Console.WriteLine($"📝 Descripción: {v.Descripcion}\n");

            Console.WriteLine("📊 Características principales:");
            Console.WriteLine($"   • Porte: {v.Porte}");
            Console.WriteLine($"   • Tamaño del grano: {v.TamanoGrano}");
            Console.WriteLine($"   • Altitud óptima: {v.AltitudOptimaM} msnm");
            Console.WriteLine($"   • Rendimiento: {v.PotencialRendimiento}");
            Console.WriteLine($"   • Nivel de calidad: {v.NivelCalidad}\n");

            Console.WriteLine("🛡 Resistencias:");
            Console.WriteLine($"   • Roya: {v.ResistenciaRoya}");
            Console.WriteLine($"   • Antracnosis: {v.ResistenciaAntracnosis}");
            Console.WriteLine($"   • Nematodos: {v.ResistenciaNematodos}\n");

            Console.WriteLine("🌱 Info agronómica:");
            Console.WriteLine($"   • Tiempo de cosecha: {v.TiempoCosecha}");
            Console.WriteLine($"   • Maduración: {v.Maduracion}");
            Console.WriteLine($"   • Notas de nutrición: {v.NotasNutricion}");
            Console.WriteLine($"   • Densidad de siembra: {v.DensidadSiembra}\n");

            Console.WriteLine("🧬 Linaje genético:");
            Console.WriteLine($"   • Obtentor: {v.Obtentor}");
            Console.WriteLine($"   • Familia: {v.Familia}");
            Console.WriteLine($"   • Grupo genético: {v.GrupoGenetico}");

            Console.WriteLine("\n╔══════════════════════════════════════════════════════╗");
            Console.WriteLine("║ Presione cualquier tecla para volver al catálogo...  ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════╝");
            Console.ReadKey();
        }
    }
}
