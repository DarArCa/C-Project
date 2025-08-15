using System.Collections.Generic;
using CoffeProject.modules.Panel.Domain.Entities;

namespace CoffeProject.modules.Panel.Application.Interfaces;

    public interface IAdmin
    {
        void CrearUsuario(string nombreUsuario, string correo, string contrasenaHash, int roleId);
        Usuario? BuscarPorNombre(string nombreUsuario);
        Usuario? BuscarPorCorreo(string correo);
        IEnumerable<Usuario> ListarUsuarios();
        void EditarUsuario(int id, string? nuevoNombreUsuario, string? nuevoCorreo, string? nuevaContrasenaHash, int? nuevoRoleId, bool? estaActivo);
        void EliminarUsuario(int id);
        IEnumerable<Rol> ListarRoles();
    }

