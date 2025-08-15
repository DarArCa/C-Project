using System;
using CoffeProject.modules.Panel.Domain.Entities;
using CoffeProject.modules.User.Application.Interfaces;
using CoffeProject.modules.User.Domain.Entities;

namespace CoffeProject.modules.User.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Registrar(UserRegisterDto dto)
        {
            if (_userRepository.ObtenerPorNombre(dto.NombreUsuario) != null)
                throw new Exception("Ya existe un usuario con ese nombre.");

            if (_userRepository.ObtenerPorCorreo(dto.Correo) != null)
                throw new Exception("Ya existe un usuario con ese correo.");

            var usuario = new Usuario
            {
                NombreUsuario = dto.NombreUsuario,
                Correo = dto.Correo,
                ContrasenaHash = dto.ContrasenaHash,
                RoleId = 3, // Asignar automáticamente rol "viewer/cliente"
                Rol = null,
                EstaActivo = true
            };

            _userRepository.RegistrarUsuario(usuario);
        }
    }
}
