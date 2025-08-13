using CoffeProject.src.modules.Users.Application.Interfaces;
using CoffeProject.src.modules.Users.Domain.Entities;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CoffeProject.src.modules.Users.Application
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> CrearUsuarioAsync(string nombreUsuario, string correo, string contrasena)
        {
            // Validar si el nombre de usuario ya existe
            if (await _userRepository.ExistsByUsernameAsync(nombreUsuario))
                throw new InvalidOperationException("El nombre de usuario ya existe.");

            // Validar si el correo ya está registrado
            if (await _userRepository.ExistsByEmailAsync(correo))
                throw new InvalidOperationException("El correo ya está registrado.");

            // Hashear la contraseña
            var hash = HashPassword(contrasena);

            // Crear usuario con rol Viewer (RoleId = 3)
            var user = User.CrearUsuarioViewer(nombreUsuario, correo, hash);

            // Guardar en base de datos
            await _userRepository.AddAsync(user);

            return user;
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hashBytes = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hashBytes);
        }
    }
}
