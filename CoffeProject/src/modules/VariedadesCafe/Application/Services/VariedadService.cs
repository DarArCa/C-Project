using CoffeProject.modules.VariedadesCafe.Application.Interfaces;
using CoffeProject.modules.VariedadesCafe.Domain.Entities;
using CoffeProject.modules.VariedadesCafe.Domain.Ports;
using System.Collections.Generic;

namespace CoffeProject.modules.VariedadesCafe.Application.Services
{
    public class VariedadService : IVariedadService
    {
        private readonly IVariedadRepository _repo;

        public VariedadService(IVariedadRepository repo)
        {
            _repo = repo;
        }

        public List<Variedad> ObtenerCatalogo() => _repo.ObtenerCatalogo();

        public Variedad? ObtenerFicha(int id) => _repo.ObtenerFicha(id);

        public List<Variedad> BuscarPorNombre(string termino) => _repo.BuscarPorNombre(termino);

        public List<Variedad> FiltrarPorPorte(string porteNombre) => _repo.FiltrarPorPorte(porteNombre);

        public List<Variedad> FiltrarPorTamano(string tamanoNombre) => _repo.FiltrarPorTamano(tamanoNombre);

        public List<Variedad> FiltrarPorAltitud(int min, int max) => _repo.FiltrarPorAltitud(min, max);

        public List<Variedad> FiltrarPorRendimiento(string rendimientoNombre) => _repo.FiltrarPorRendimiento(rendimientoNombre);

        public List<Variedad> FiltrarPorCalidad(int nivel) => _repo.FiltrarPorCalidad(nivel);

        public List<Variedad> FiltrarPorResistencia(string tipoResistencia, string nivelResistencia) =>
            _repo.FiltrarPorResistencia(tipoResistencia, nivelResistencia);

        public List<Variedad> FiltrarPorEtiqueta(string etiqueta) => _repo.FiltrarPorEtiqueta(etiqueta);
    }
}
