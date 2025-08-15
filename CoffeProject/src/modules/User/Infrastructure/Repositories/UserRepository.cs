using System.Linq;
using CoffeProject.modules.Panel.Domain.Entities;
using CoffeProject.modules.User.Application.Interfaces;
using CoffeProject.shared.Context;
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

        public void RegistrarUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }

        public Usuario? ObtenerPorNombre(string nombreUsuario)
        {
            return _context.Usuarios.FirstOrDefault(u => u.NombreUsuario == nombreUsuario);
        }

        public Usuario? ObtenerPorCorreo(string correo)
        {
            return _context.Usuarios.FirstOrDefault(u => u.Correo == correo);
        }
    }
}
