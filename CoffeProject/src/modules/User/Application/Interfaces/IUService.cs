using CoffeProject.src.modules.Users.Domain.Entities;
using System.Threading.Tasks;

namespace CoffeProject.src.modules.Users.Application
{
    public interface IUserService
    {
        Task<User> CrearUsuarioAsync(string nombreUsuario, string correo, string contrasena);
    }
}
