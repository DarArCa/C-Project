using CoffeProject.modules.User.Application.Interfaces;
using System;

namespace CoffeProject.modules.User.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

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
