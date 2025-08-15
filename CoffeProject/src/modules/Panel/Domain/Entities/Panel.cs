namespace CoffeProject.modules.Panel.Domain.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string ContrasenaHash { get; set; } = string.Empty;

        public int RoleId { get; set; }
        public Rol Rol { get; set; }
        public bool EstaActivo { get; set; } = true;
    }
}
