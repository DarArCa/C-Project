using CoffeProject.modules.User.Domain.Entities;

namespace CoffeProject.modules.User.Application.Interfaces
{
    public interface IUserRepository
    {
        Usuario? ObtenerPorId(int id);
        Usuario? ObtenerPorUsuarioYContrasena(string nombreUsuario, string contrasenaHash);
    }
}
