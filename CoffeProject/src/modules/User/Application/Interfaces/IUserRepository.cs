using CoffeProject.modules.Panel.Domain.Entities;

namespace CoffeProject.modules.User.Application.Interfaces
{
    public interface IUserRepository
    {
        void RegistrarUsuario(Usuario usuario);
        Usuario? ObtenerPorNombre(string nombreUsuario);
        Usuario? ObtenerPorCorreo(string correo);
    }
}
