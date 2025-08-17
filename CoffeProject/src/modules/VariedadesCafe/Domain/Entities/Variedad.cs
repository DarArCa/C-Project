namespace CoffeProject.modules.VariedadesCafe.Domain.Entities
{
    public class Variedad
    {
        public int Id { get; set; }
        public string NombreComun { get; set; } = "";
        public string NombreCientifico { get; set; } = "";
        public string ImagenReferencia { get; set; } = "";   // URL o ruta local
        public string Descripcion { get; set; } = "";

        // Características principales
        public string Porte { get; set; } = "";
        public string TamanoGrano { get; set; } = "";
        public int AltitudOptimaM { get; set; }
        public string PotencialRendimiento { get; set; } = "";
        public int NivelCalidad { get; set; }

        // Resistencias
        public string ResistenciaRoya { get; set; } = "";
        public string ResistenciaAntracnosis { get; set; } = "";
        public string ResistenciaNematodos { get; set; } = "";

        // Info agronómica
        public string TiempoCosecha { get; set; } = "";
        public string Maduracion { get; set; } = "";
        public string NotasNutricion { get; set; } = "";
        public string DensidadSiembra { get; set; } = "";

        // Historia y linaje genético
        public string Obtentor { get; set; } = "";
        public string Familia { get; set; } = "";
        public string GrupoGenetico { get; set; } = "";
    }
}
