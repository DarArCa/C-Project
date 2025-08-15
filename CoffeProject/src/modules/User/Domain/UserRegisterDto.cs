namespace CoffeProject.modules.User.Domain.Entities
{
    public class UserRegisterDto
    {
        public string NombreUsuario { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string ContrasenaHash { get; set; } = string.Empty;
    }
}
