namespace CoffeProject.modules.Panel.Application.Interfaces
{
    public interface IPanelService
    {
        bool VerificarRol(int usuarioId, string nombreRol);
        int? LoginYObtenerId(string nombreUsuario, string contrasenaHash);
    }
}
