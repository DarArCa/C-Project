using System.Collections.Generic;
using CoffeProject.modules.VariedadesCafe.Application.Interfaces;
using CoffeProject.modules.VariedadesCafe.Domain.Entities;


namespace CoffeProject.modules.VariedadesCafe.Application.Services
{
    public class VariedadService
    {
        private readonly IVariedadRepository _repository;

        public VariedadService(IVariedadRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Variedad> ObtenerCatalogo()
        {
            return _repository.ObtenerCatalogo();
        }

        public Variedad? ObtenerFicha(int id)
        {
            return _repository.ObtenerFicha(id);
        }
    }
}
