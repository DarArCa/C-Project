namespace CoffeProject.modules.User.Application.Interfaces
{
    public interface IUserService
    {
        bool VerificarRol(int usuarioId, string nombreRol);
        int? LoginYObtenerId(string nombreUsuario, string contrasenaHash);
    }
}
