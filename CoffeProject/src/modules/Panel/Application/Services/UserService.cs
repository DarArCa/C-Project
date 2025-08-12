using CoffeProject.modules.Panel.Application.Interfaces;
using System;

namespace CoffeProject.modules.Panel.Application.Services
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;

        public bool VerificarRol(int usuarioId, string nombreRol)
        {
            var usuario = _userRepository.ObtenerPorId(usuarioId);
            return usuario != null &&
                   usuario.Rol != null &&
                   usuario.Rol.Nombre.Equals(nombreRol, StringComparison.OrdinalIgnoreCase);
        }

        public int? LoginYObtenerId(string nombreUsuario, string contrasenaHash)
        {
            var u = _userRepository.ObtenerPorUsuarioYContrasena(nombreUsuario, contrasenaHash);
            return u?.Id;
        }
    }
}
