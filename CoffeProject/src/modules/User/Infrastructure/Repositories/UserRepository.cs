using System.Linq;
using CoffeProject.shared.Context;
using CoffeProject.modules.User.Domain.Entities;
using CoffeProject.modules.User.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CoffeProject.modules.User.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public Usuario? ObtenerPorId(int id)
        {
            return _context.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefault(u => u.Id == id);
        }

        public Usuario? ObtenerPorUsuarioYContrasena(string nombreUsuario, string contrasenaHash)
        {
            return _context.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefault(u => u.NombreUsuario == nombreUsuario && u.ContrasenaHash == contrasenaHash);
        }
    }
}
