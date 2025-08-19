using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using CoffeProject.modules.VariedadesCafe.Domain.Entities;
using CoffeProject.modules.VariedadesCafe.Domain.Ports;
using CoffeProject.Helpers; // 👈 para VariedadMapper y ResistenciaEtiquetaHelper

namespace CoffeProject.modules.VariedadesCafe.Infrastructure.Repositories
{
    public class VariedadRepository : IVariedadRepository
    {
        private readonly string _conn;

        public VariedadRepository(string connectionString)
        {
            _conn = connectionString;
        }

        public List<Variedad> ObtenerCatalogo()
        {
            var lista = new List<Variedad>();
            using var conn = new MySqlConnection(_conn);
            conn.Open();

            string sql = @"
                SELECT v.Id, v.NombreComun, v.NombreCientifico, v.Descripcion,
                       v.PorteId, pv.Nombre AS PorteNombre,
                       v.GrainSizeId, tg.Nombre AS GrainSizeNombre,
                       v.AltitudOptimaM, v.YieldPotentialId, pr.Nombre AS PotencialRendimiento,
                       v.QualityLevelId, nl.Nivel AS NivelCalidad,
                       v.Obtentor, v.Familia, v.GrupoGenetico,
                       v.TiempoCosecha, v.Maduracion, v.NotasNutricion, v.DensidadSiembra,
                       (SELECT RutaArchivo FROM ImagenesVariedad iv WHERE iv.VariedadId = v.Id AND iv.EsPrincipal = 1 LIMIT 1) AS ImagenReferencia
                FROM Variedades v
                LEFT JOIN Porte pv ON pv.Id = v.PorteId
                LEFT JOIN TamanoGrano tg ON tg.Id = v.GrainSizeId
                LEFT JOIN PotencialRendimiento pr ON pr.Id = v.YieldPotentialId
                LEFT JOIN NivelCalidad nl ON nl.Id = v.QualityLevelId
                ORDER BY v.NombreComun;
            ";

            using var cmd = new MySqlCommand(sql, conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                lista.Add(VariedadMapper.Map(reader));

            reader.Close();

            foreach (var v in lista)
                ResistenciaEtiquetaHelper.Fill(conn, v);

            return lista;
        }

        public Variedad? ObtenerFicha(int id)
        {
            using var conn = new MySqlConnection(_conn);
            conn.Open();

            string sql = @"
                SELECT v.Id, v.NombreComun, v.NombreCientifico, v.Descripcion,
                       v.PorteId, pv.Nombre AS PorteNombre,
                       v.GrainSizeId, tg.Nombre AS GrainSizeNombre,
                       v.AltitudOptimaM, v.YieldPotentialId, pr.Nombre AS PotencialRendimiento,
                       v.QualityLevelId, nl.Nivel AS NivelCalidad,
                       v.Obtentor, v.Familia, v.GrupoGenetico,
                       v.TiempoCosecha, v.Maduracion, v.NotasNutricion, v.DensidadSiembra,
                       (SELECT RutaArchivo FROM ImagenesVariedad iv WHERE iv.VariedadId = v.Id AND iv.EsPrincipal = 1 LIMIT 1) AS ImagenReferencia
                FROM Variedades v
                LEFT JOIN Porte pv ON pv.Id = v.PorteId
                LEFT JOIN TamanoGrano tg ON tg.Id = v.GrainSizeId
                LEFT JOIN PotencialRendimiento pr ON pr.Id = v.YieldPotentialId
                LEFT JOIN NivelCalidad nl ON nl.Id = v.QualityLevelId
                WHERE v.Id = @Id
                LIMIT 1;
            ";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Id", id);

            using var reader = cmd.ExecuteReader();
            if (!reader.Read()) return null;

            var v = VariedadMapper.Map(reader);
            reader.Close();

            ResistenciaEtiquetaHelper.Fill(conn, v);
            return v;
        }

        public List<Variedad> BuscarPorNombre(string termino)
        {
            var lista = new List<Variedad>();
            using var conn = new MySqlConnection(_conn);
            conn.Open();

            string sql = @"
                SELECT v.Id, v.NombreComun, v.NombreCientifico, v.Descripcion,
                       v.PorteId, pv.Nombre AS PorteNombre,
                       v.GrainSizeId, tg.Nombre AS GrainSizeNombre,
                       v.AltitudOptimaM, v.YieldPotentialId, pr.Nombre AS PotencialRendimiento,
                       v.QualityLevelId, nl.Nivel AS NivelCalidad,
                       v.Obtentor, v.Familia, v.GrupoGenetico,
                       v.TiempoCosecha, v.Maduracion, v.NotasNutricion, v.DensidadSiembra,
                       (SELECT RutaArchivo FROM ImagenesVariedad iv WHERE iv.VariedadId = v.Id AND iv.EsPrincipal = 1 LIMIT 1) AS ImagenReferencia
                FROM Variedades v
                LEFT JOIN Porte pv ON pv.Id = v.PorteId
                LEFT JOIN TamanoGrano tg ON tg.Id = v.GrainSizeId
                LEFT JOIN PotencialRendimiento pr ON pr.Id = v.YieldPotentialId
                LEFT JOIN NivelCalidad nl ON nl.Id = v.QualityLevelId
                WHERE v.NombreComun LIKE @t OR v.NombreCientifico LIKE @t
                ORDER BY v.NombreComun;
            ";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@t", "%" + termino + "%");

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                lista.Add(VariedadMapper.Map(reader));

            reader.Close();

            foreach (var v in lista)
                ResistenciaEtiquetaHelper.Fill(conn, v);

            return lista;
        }

        // ==== Filtros simples ====
        public List<Variedad> FiltrarPorPorte(string porteNombre) => FiltrarGenerico("pv.Nombre = @val", ("@val", porteNombre));
        public List<Variedad> FiltrarPorTamano(string tamanoNombre) => FiltrarGenerico("tg.Nombre = @val", ("@val", tamanoNombre));
        public List<Variedad> FiltrarPorAltitud(int min, int max) => FiltrarGenerico("v.AltitudOptimaM BETWEEN @min AND @max", ("@min", min), ("@max", max));
        public List<Variedad> FiltrarPorRendimiento(string rendimientoNombre) => FiltrarGenerico("pr.Nombre = @val", ("@val", rendimientoNombre));
        public List<Variedad> FiltrarPorCalidad(int nivel) => FiltrarGenerico("nl.Nivel = @val", ("@val", nivel));

        // ==== Filtros con joins extra ====
        public List<Variedad> FiltrarPorResistencia(string tipoResistencia, string nivelResistencia)
        {
            var lista = new List<Variedad>();
            using var conn = new MySqlConnection(_conn);
            conn.Open();

            string sql = @"
                SELECT DISTINCT v.Id, v.NombreComun, v.NombreCientifico, v.Descripcion,
                       v.PorteId, pv.Nombre AS PorteNombre,
                       v.GrainSizeId, tg.Nombre AS GrainSizeNombre,
                       v.AltitudOptimaM, v.YieldPotentialId, pr.Nombre AS PotencialRendimiento,
                       v.QualityLevelId, nl.Nivel AS NivelCalidad,
                       v.Obtentor, v.Familia, v.GrupoGenetico,
                       v.TiempoCosecha, v.Maduracion, v.NotasNutricion, v.DensidadSiembra,
                       (SELECT RutaArchivo FROM ImagenesVariedad iv WHERE iv.VariedadId = v.Id AND iv.EsPrincipal = 1 LIMIT 1) AS ImagenReferencia
                FROM Variedades v
                LEFT JOIN Porte pv ON pv.Id = v.PorteId
                LEFT JOIN TamanoGrano tg ON tg.Id = v.GrainSizeId
                LEFT JOIN PotencialRendimiento pr ON pr.Id = v.YieldPotentialId
                LEFT JOIN NivelCalidad nl ON nl.Id = v.QualityLevelId
                INNER JOIN ResistenciasVariedad rv ON rv.VariedadId = v.Id
                INNER JOIN TipoResistencia tr ON tr.Id = rv.TipoResistenciaId
                INNER JOIN NivelResistencia nr ON nr.Id = rv.NivelResistenciaId
                WHERE tr.Nombre = @tipo AND nr.Nombre = @nivel
                ORDER BY v.NombreComun;
            ";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@tipo", tipoResistencia);
            cmd.Parameters.AddWithValue("@nivel", nivelResistencia);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                lista.Add(VariedadMapper.Map(reader));

            reader.Close();

            foreach (var v in lista)
                ResistenciaEtiquetaHelper.Fill(conn, v);

            return lista;
        }

        public List<Variedad> FiltrarPorEtiqueta(string etiqueta)
        {
            var lista = new List<Variedad>();
            using var conn = new MySqlConnection(_conn);
            conn.Open();

            string sql = @"
                SELECT DISTINCT v.Id, v.NombreComun, v.NombreCientifico, v.Descripcion,
                       v.PorteId, pv.Nombre AS PorteNombre,
                       v.GrainSizeId, tg.Nombre AS GrainSizeNombre,
                       v.AltitudOptimaM, v.YieldPotentialId, pr.Nombre AS PotencialRendimiento,
                       v.QualityLevelId, nl.Nivel AS NivelCalidad,
                       v.Obtentor, v.Familia, v.GrupoGenetico,
                       v.TiempoCosecha, v.Maduracion, v.NotasNutricion, v.DensidadSiembra,
                       (SELECT RutaArchivo FROM ImagenesVariedad iv WHERE iv.VariedadId = v.Id AND iv.EsPrincipal = 1 LIMIT 1) AS ImagenReferencia
                FROM Variedades v
                LEFT JOIN Porte pv ON pv.Id = v.PorteId
                LEFT JOIN TamanoGrano tg ON tg.Id = v.GrainSizeId
                LEFT JOIN PotencialRendimiento pr ON pr.Id = v.YieldPotentialId
                LEFT JOIN NivelCalidad nl ON nl.Id = v.QualityLevelId
                INNER JOIN EtiquetasVariedad ev ON ev.VariedadId = v.Id
                INNER JOIN Etiquetas e ON e.Id = ev.EtiquetaId
                WHERE e.Nombre = @et
                ORDER BY v.NombreComun;
            ";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@et", etiqueta);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                lista.Add(VariedadMapper.Map(reader));

            reader.Close();

            foreach (var v in lista)
                ResistenciaEtiquetaHelper.Fill(conn, v);

            return lista;
        }

        // ==== Helper genérico para filtros simples ====
        private List<Variedad> FiltrarGenerico(string whereClause, params (string, object)[] parametros)
        {
            var lista = new List<Variedad>();
            using var conn = new MySqlConnection(_conn);
            conn.Open();

            string sql = $@"
                SELECT v.Id, v.NombreComun, v.NombreCientifico, v.Descripcion,
                       v.PorteId, pv.Nombre AS PorteNombre,
                       v.GrainSizeId, tg.Nombre AS GrainSizeNombre,
                       v.AltitudOptimaM, v.YieldPotentialId, pr.Nombre AS PotencialRendimiento,
                       v.QualityLevelId, nl.Nivel AS NivelCalidad,
                       v.Obtentor, v.Familia, v.GrupoGenetico,
                       v.TiempoCosecha, v.Maduracion, v.NotasNutricion, v.DensidadSiembra,
                       (SELECT RutaArchivo FROM ImagenesVariedad iv WHERE iv.VariedadId = v.Id AND iv.EsPrincipal = 1 LIMIT 1) AS ImagenReferencia
                FROM Variedades v
                LEFT JOIN Porte pv ON pv.Id = v.PorteId
                LEFT JOIN TamanoGrano tg ON tg.Id = v.GrainSizeId
                LEFT JOIN PotencialRendimiento pr ON pr.Id = v.YieldPotentialId
                LEFT JOIN NivelCalidad nl ON nl.Id = v.QualityLevelId
                WHERE {whereClause}
                ORDER BY v.NombreComun;
            ";

            using var cmd = new MySqlCommand(sql, conn);
            foreach (var p in parametros)
                cmd.Parameters.AddWithValue(p.Item1, p.Item2);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                lista.Add(VariedadMapper.Map(reader));

            reader.Close();

            foreach (var v in lista)
                ResistenciaEtiquetaHelper.Fill(conn, v);

            return lista;
        }
    }
}
