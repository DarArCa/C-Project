using System.Collections.Generic;
using CoffeProject.modules.VariedadesCafe.Domain.Entities;
namespace CoffeProject.modules.VariedadesCafe.Application.Interfaces
{
    public interface IVariedadRepository
    {
        List<Variedad> ObtenerCatalogo();
        Variedad? ObtenerFicha(int id);
    }
}
