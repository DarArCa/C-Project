using MySql.Data.MySqlClient;
using CoffeProject.modules.VariedadesCafe.Domain.Entities;

namespace CoffeProject.Helpers
{
    public static class ResistenciaEtiquetaHelper
    {
        public static void Fill(MySqlConnection conn, Variedad v)
        {
            // === Resistencias ===
            string sqlRes = @"
                SELECT tr.Nombre as Tipo, nr.Nombre as Nivel
                FROM ResistenciasVariedad rv
                JOIN TipoResistencia tr ON tr.Id = rv.TipoResistenciaId
                JOIN NivelResistencia nr ON nr.Id = rv.NivelResistenciaId
                WHERE rv.VariedadId = @id;
            ";

            using (var cmd = new MySqlCommand(sqlRes, conn))
            {
                cmd.Parameters.AddWithValue("@id", v.Id);
                using var r = cmd.ExecuteReader();
                while (r.Read())
                {
                    var tipo = r.IsDBNull(r.GetOrdinal("Tipo")) ? "" : r.GetString("Tipo");
                    var nivel = r.IsDBNull(r.GetOrdinal("Nivel")) ? "" : r.GetString("Nivel");

                    if (tipo.Equals("Roya", StringComparison.OrdinalIgnoreCase)) v.ResistenciaRoya = nivel;
                    if (tipo.Equals("Antracnosis", StringComparison.OrdinalIgnoreCase)) v.ResistenciaAntracnosis = nivel;
                    if (tipo.Equals("Nematodos", StringComparison.OrdinalIgnoreCase)) v.ResistenciaNematodos = nivel;
                }
                r.Close();
            }

            // === Etiquetas ===
            v.Etiquetas.Clear();
            string sqlEtiq = @"
                SELECT e.Nombre
                FROM EtiquetasVariedad ev
                JOIN Etiquetas e ON e.Id = ev.EtiquetaId
                WHERE ev.VariedadId = @id;
            ";

            using (var cmd2 = new MySqlCommand(sqlEtiq, conn))
            {
                cmd2.Parameters.AddWithValue("@id", v.Id);
                using var r2 = cmd2.ExecuteReader();
                while (r2.Read())
                {
                    v.Etiquetas.Add(r2.IsDBNull(0) ? "" : r2.GetString(0));
                }
                r2.Close();
            }
        }
    }
}
