using System.Data;
using CoffeProject.modules.VariedadesCafe.Domain.Entities;

namespace CoffeProject.Helpers
{
    public static class VariedadMapper
    {
        public static Variedad Map(IDataRecord r)
        {
            return new Variedad
            {
                Id = r.GetInt32(r.GetOrdinal("Id")),
                NombreComun = r.IsDBNull(r.GetOrdinal("NombreComun")) ? "" : r.GetString(r.GetOrdinal("NombreComun")),
                NombreCientifico = r.IsDBNull(r.GetOrdinal("NombreCientifico")) ? "" : r.GetString(r.GetOrdinal("NombreCientifico")),
                Descripcion = r.IsDBNull(r.GetOrdinal("Descripcion")) ? "" : r.GetString(r.GetOrdinal("Descripcion")),
                Porte = r.IsDBNull(r.GetOrdinal("PorteNombre")) ? "" : r.GetString(r.GetOrdinal("PorteNombre")),
                TamanoGrano = r.IsDBNull(r.GetOrdinal("GrainSizeNombre")) ? "" : r.GetString(r.GetOrdinal("GrainSizeNombre")),
                AltitudOptimaM = r.IsDBNull(r.GetOrdinal("AltitudOptimaM")) ? 0 : r.GetInt32(r.GetOrdinal("AltitudOptimaM")),
                PotencialRendimiento = r.IsDBNull(r.GetOrdinal("PotencialRendimiento")) ? "" : r.GetString(r.GetOrdinal("PotencialRendimiento")),
                NivelCalidad = r.IsDBNull(r.GetOrdinal("NivelCalidad")) ? 0 : Convert.ToInt32(r["NivelCalidad"]),
                Obtentor = r.IsDBNull(r.GetOrdinal("Obtentor")) ? "" : r.GetString(r.GetOrdinal("Obtentor")),
                Familia = r.IsDBNull(r.GetOrdinal("Familia")) ? "" : r.GetString(r.GetOrdinal("Familia")),
                GrupoGenetico = r.IsDBNull(r.GetOrdinal("GrupoGenetico")) ? "" : r.GetString(r.GetOrdinal("GrupoGenetico")),
                TiempoCosecha = r.IsDBNull(r.GetOrdinal("TiempoCosecha")) ? "" : r.GetString(r.GetOrdinal("TiempoCosecha")),
                Maduracion = r.IsDBNull(r.GetOrdinal("Maduracion")) ? "" : r.GetString(r.GetOrdinal("Maduracion")),
                NotasNutricion = r.IsDBNull(r.GetOrdinal("NotasNutricion")) ? "" : r.GetString(r.GetOrdinal("NotasNutricion")),
                DensidadSiembra = r.IsDBNull(r.GetOrdinal("DensidadSiembra")) ? "" : r.GetString(r.GetOrdinal("DensidadSiembra")),
                ImagenReferencia = r.IsDBNull(r.GetOrdinal("ImagenReferencia")) ? "" : r.GetString(r.GetOrdinal("ImagenReferencia"))
            };
        }
    }
}
