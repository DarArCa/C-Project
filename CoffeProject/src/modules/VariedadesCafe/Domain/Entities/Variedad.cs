using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeProject.modules.VariedadesCafe.Domain.Entities
{
    public class Variedad
    {
        public int Id { get; set; }
        public string NombreComun { get; set; } = string.Empty;
        public string NombreCientifico { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;

        // FK ids (según tu schema)
        public int? PorteId { get; set; }
        public int? GrainSizeId { get; set; }
        public int? AltitudOptimaM { get; set; }
        public int? YieldPotentialId { get; set; }
        public int? QualityLevelId { get; set; }

        // Datos agronómicos / genéticos
        public string Obtentor { get; set; } = string.Empty;
        public string Familia { get; set; } = string.Empty;
        public string GrupoGenetico { get; set; } = string.Empty;
        public string TiempoCosecha { get; set; } = string.Empty;
        public string Maduracion { get; set; } = string.Empty;
        public string NotasNutricion { get; set; } = string.Empty;
        public string DensidadSiembra { get; set; } = string.Empty;

        // Campos "amigables" (llenados por repositorio con joins)
        [NotMapped] public string Porte { get; set; } = string.Empty;
        [NotMapped] public string TamanoGrano { get; set; } = string.Empty;
        [NotMapped] public string PotencialRendimiento { get; set; } = string.Empty;
        [NotMapped] public int NivelCalidad { get; set; } = 0;
        [NotMapped] public string ImagenReferencia { get; set; } = string.Empty;

        // Resistencias (valores como "Susceptible", "Tolerante", "Resistente")
        [NotMapped] public string ResistenciaRoya { get; set; } = string.Empty;
        [NotMapped] public string ResistenciaAntracnosis { get; set; } = string.Empty;
        [NotMapped] public string ResistenciaNematodos { get; set; } = string.Empty;

        // Etiquetas (tags)
        [NotMapped] public List<string> Etiquetas { get; set; } = new List<string>();
    }
}
