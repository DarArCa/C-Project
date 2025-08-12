using CoffeProject.modules.Panel.Domain.Entities;

namespace CoffeProject.modules.Panel.Application.Interfaces
{
    public interface IPanelRepository
    {
        Usuario? ObtenerPorId(int id);
        Usuario? ObtenerPorUsuarioYContrasena(string nombreUsuario, string contrasenaHash);
    }
}
