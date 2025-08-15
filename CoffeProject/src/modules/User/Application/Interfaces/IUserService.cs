using CoffeProject.modules.User.Domain.Entities;

namespace CoffeProject.modules.User.Application.Interfaces
{
    public interface IUserService
    {
        void Registrar(UserRegisterDto dto);
    }
}
