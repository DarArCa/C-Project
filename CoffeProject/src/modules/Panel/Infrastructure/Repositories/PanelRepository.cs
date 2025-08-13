using System.Linq;
using CoffeProject.shared.Context;
using CoffeProject.modules.Panel.Domain.Entities;
using CoffeProject.modules.Panel.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CoffeProject.modules.Panel.Infrastructure.Repositories
{
    public class PanelRepository(AppDbContext context) : IPanelRepository
    {
        private readonly AppDbContext _context = context;

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
