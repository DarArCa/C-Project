using CoffeProject.modules.Panel.Domain.Entities; 
using CoffeProject.src.modules.Users.Application.Interfaces;
using CoffeProject.src.modules.Users.Domain.Entities;
using CoffeProject.shared.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CoffeProject.src.modules.Users.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user)
        {
            // Mapear entidad de dominio User -> entidad de infraestructura Usuario
            var usuario = new Usuario
            {
                NombreUsuario = user.NombreUsuario,
                Correo = user.Correo,
                ContrasenaHash = user.ContrasenaHash,
                RoleId = user.RoleId,
                EstaActivo = user.EstaActivo
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsByEmailAsync(string correo)
        {
            return await _context.Usuarios.AnyAsync(u => u.Correo == correo);
        }

        public async Task<bool> ExistsByUsernameAsync(string nombreUsuario)
        {
            return await _context.Usuarios.AnyAsync(u => u.NombreUsuario == nombreUsuario);
        }
    }
}
