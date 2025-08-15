using System.Collections.Generic;
using CoffeProject.modules.Panel.Application.Interfaces;
using CoffeProject.modules.Panel.Domain.Entities;

namespace CoffeProject.modules.Panel.Application.Services
{
    public class AdminService : IAdmin
    {
        private readonly IAdminRepository _adminRepository;

        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public void CrearUsuario(string nombreUsuario, string correo, string contrasenaHash, int roleId)
        {
            
            if (_adminRepository.ObtenerPorNombre(nombreUsuario) != null)
                throw new Exception("Ya existe un usuario con ese nombre.");

            if (_adminRepository.ObtenerPorCorreo(correo) != null)
                throw new Exception("Ya existe un usuario con ese correo.");
                    var usuario = new Usuario
            {
                NombreUsuario = nombreUsuario,
                Correo = correo,
                ContrasenaHash = contrasenaHash,
                RoleId = roleId,
                EstaActivo = true
            };
            _adminRepository.Agregar(usuario);
        }

        public Usuario? BuscarPorNombre(string nombreUsuario)
        {
            return _adminRepository.ObtenerPorNombre(nombreUsuario);
        }

        public Usuario? BuscarPorCorreo(string correo)
        {
            return _adminRepository.ObtenerPorCorreo(correo);
        }

        public IEnumerable<Usuario> ListarUsuarios()
        {
            return _adminRepository.ObtenerTodos();
        }

        public void EditarUsuario(int id, string? nuevoNombreUsuario, string? nuevoCorreo, string? nuevaContrasenaHash, int? nuevoRoleId, bool? estaActivo)
        {
            var usuario = _adminRepository.ObtenerPorId(id);
            if (usuario == null) return;

            if (!string.IsNullOrEmpty(nuevoNombreUsuario)) usuario.NombreUsuario = nuevoNombreUsuario;
            if (!string.IsNullOrEmpty(nuevoCorreo)) usuario.Correo = nuevoCorreo;
            if (!string.IsNullOrEmpty(nuevaContrasenaHash)) usuario.ContrasenaHash = nuevaContrasenaHash;
            if (nuevoRoleId.HasValue) usuario.RoleId = nuevoRoleId.Value;
            if (estaActivo.HasValue) usuario.EstaActivo = estaActivo.Value;

            _adminRepository.Actualizar(usuario);
        }

        public void EliminarUsuario(int id)
        {
            _adminRepository.Eliminar(id);
        }

        public IEnumerable<Rol> ListarRoles()
        {
            return _adminRepository.ObtenerRoles();
        }
    }
}
