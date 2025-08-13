using CoffeProject.modules.Panel.Application.Interfaces;
using System;

namespace CoffeProject.modules.Panel.Application.Services
{
    public class PanelService(IPanelRepository PanelRepository) : IPanelService
    {
        private readonly IPanelRepository _panelRepository = PanelRepository;

        public bool VerificarRol(int usuarioId, string nombreRol)
        {
            var usuario = _panelRepository.ObtenerPorId(usuarioId);
            return usuario != null &&
                   usuario.Rol != null &&
                   usuario.Rol.Nombre.Equals(nombreRol, StringComparison.OrdinalIgnoreCase);
        }

        public int? LoginYObtenerId(string nombreUsuario, string contrasenaHash)
        {
            var u = _panelRepository.ObtenerPorUsuarioYContrasena(nombreUsuario, contrasenaHash);
            return u?.Id;
        }
    }
}
