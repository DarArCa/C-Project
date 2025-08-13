using System;

namespace CoffeProject.src.modules.Users.Domain.Entities
{
    public class User
    {
        public int Id { get; private set; }
        public string NombreUsuario { get; private set; } =string.Empty;
        public string Correo { get; private set; } =string.Empty ;
        public string ContrasenaHash { get; private set; }=string.Empty ;
        public int RoleId { get; private set; }
        public bool EstaActivo { get; private set; }
        public DateTime CreadoEn { get; private set; }
        public DateTime ActualizadoEn { get; private set; }

        private User() { }

        public static User CrearUsuarioViewer(string nombreUsuario, string correo, string contrasenaHash)
        {
            return new User
            {
                NombreUsuario = nombreUsuario,
                Correo = correo,
                ContrasenaHash = contrasenaHash,
                RoleId = 3, 
                EstaActivo = true,
                CreadoEn = DateTime.UtcNow,
                ActualizadoEn = DateTime.UtcNow
            };
        }
    }
}
