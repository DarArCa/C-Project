using CoffeProject.modules.VariedadesCafe.Domain.Entities;
using System.Collections.Generic;

namespace CoffeProject.modules.VariedadesCafe.Domain.Ports
{
    public interface IVariedadRepository
    {
        List<Variedad> ObtenerCatalogo();
        Variedad? ObtenerFicha(int id);
        List<Variedad> BuscarPorNombre(string termino);
        List<Variedad> FiltrarPorPorte(string porteNombre);
        List<Variedad> FiltrarPorTamano(string tamanoNombre);
        List<Variedad> FiltrarPorAltitud(int min, int max);
        List<Variedad> FiltrarPorRendimiento(string rendimientoNombre);
        List<Variedad> FiltrarPorCalidad(int nivel);
        List<Variedad> FiltrarPorResistencia(string tipoResistencia, string nivelResistencia);
        List<Variedad> FiltrarPorEtiqueta(string etiqueta);
    }
}
