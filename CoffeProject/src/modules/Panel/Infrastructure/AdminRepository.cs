using System.Collections.Generic;
using System.Linq;
using CoffeProject.modules.Panel.Domain.Entities;
using CoffeProject.modules.Panel.Application.Interfaces;
using CoffeProject.shared.Context;
using Microsoft.EntityFrameworkCore;

namespace CoffeProject.modules.Panel.Infrastructure.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly AppDbContext _context;

        public AdminRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Agregar(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }

        public Usuario? ObtenerPorId(int id)
        {
            return _context.Usuarios.Include(u => u.Rol).FirstOrDefault(u => u.Id == id);
        }

        public Usuario? ObtenerPorNombre(string nombreUsuario)
        {
            return _context.Usuarios.Include(u => u.Rol).FirstOrDefault(u => u.NombreUsuario == nombreUsuario);
        }

        public Usuario? ObtenerPorCorreo(string correo)
        {
            return _context.Usuarios.Include(u => u.Rol).FirstOrDefault(u => u.Correo == correo);
        }

        public IEnumerable<Usuario> ObtenerTodos()
        {
            return _context.Usuarios.Include(u => u.Rol).ToList();
        }

        public void Actualizar(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            _context.SaveChanges();
        }

        public void Eliminar(int id)
        {
            var usuario = _context.Usuarios.Find(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Rol> ObtenerRoles()
        {
            return _context.Roles.ToList();
        }
    }
}
