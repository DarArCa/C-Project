using CoffeProject.modules.VariedadesCafe.Domain.Entities;
using System.Collections.Generic;

namespace CoffeProject.modules.VariedadesCafe.Application.Interfaces
{
    public interface IVariedadService
    {
        List<Variedad> ObtenerCatalogo();
        Variedad? ObtenerFichaTecnica(int id);
    }
}
