using CoffeProject.src.modules.Users.Domain.Entities;
using System.Threading.Tasks;

namespace CoffeProject.src.modules.Users.Application.Interfaces
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        Task<bool> ExistsByEmailAsync(string correo);
        Task<bool> ExistsByUsernameAsync(string nombreUsuario);
    }
}
