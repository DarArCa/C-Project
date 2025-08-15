using System.Collections.Generic;
using CoffeProject.modules.Panel.Domain.Entities;

namespace CoffeProject.modules.Panel.Application.Interfaces
{
    public interface IAdminRepository
    {
        void Agregar(Usuario usuario);
        Usuario? ObtenerPorId(int id);
        Usuario? ObtenerPorNombre(string nombreUsuario);
        Usuario? ObtenerPorCorreo(string correo);
        IEnumerable<Usuario> ObtenerTodos();
        void Actualizar(Usuario usuario);
        void Eliminar(int id);
        IEnumerable<Rol> ObtenerRoles();
    }
}
