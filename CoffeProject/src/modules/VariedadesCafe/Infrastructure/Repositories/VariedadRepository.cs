using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using CoffeProject.modules.VariedadesCafe.Domain.Entities;
using CoffeProject.modules.VariedadesCafe.Application.Interfaces;

namespace CoffeProject.modules.VariedadesCafe.Infrastructure.Repositories
{
    public class VariedadRepository : IVariedadRepository
    {
        private readonly string _connectionString;

        public VariedadRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Variedad> ObtenerCatalogo()
        {
            var variedades = new List<Variedad>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                var query = @"SELECT Id, NombreComun, NombreCientifico, Descripcion, 
                                     PorteId, GrainSizeId, AltitudOptimaM, YieldPotentialId, 
                                     QualityLevelId, Obtentor, Familia, GrupoGenetico, 
                                     TiempoCosecha, Maduracion, NotasNutricion, DensidadSiembra
                              FROM Variedades";

                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        variedades.Add(new Variedad
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            NombreComun = reader.GetString(reader.GetOrdinal("NombreComun")),
                            NombreCientifico = reader.GetString(reader.GetOrdinal("NombreCientifico")),
                            Descripcion = reader.IsDBNull(reader.GetOrdinal("Descripcion")) ? "" : reader.GetString(reader.GetOrdinal("Descripcion")),
                            Porte = reader.IsDBNull(reader.GetOrdinal("PorteId")) ? "" : reader.GetString(reader.GetOrdinal("PorteId")),
                            TamanoGrano = reader.IsDBNull(reader.GetOrdinal("GrainSizeId")) ? "" : reader.GetString(reader.GetOrdinal("GrainSizeId")),
                            AltitudOptimaM = reader.IsDBNull(reader.GetOrdinal("AltitudOptimaM")) ? 0 : reader.GetInt32(reader.GetOrdinal("AltitudOptimaM")),
                            PotencialRendimiento = reader.IsDBNull(reader.GetOrdinal("YieldPotentialId")) ? "" : reader.GetString(reader.GetOrdinal("YieldPotentialId")),
                            NivelCalidad = reader.IsDBNull(reader.GetOrdinal("QualityLevelId")) ? 0 : reader.GetInt32(reader.GetOrdinal("QualityLevelId")),
                            Obtentor = reader.IsDBNull(reader.GetOrdinal("Obtentor")) ? "" : reader.GetString(reader.GetOrdinal("Obtentor")),
                            Familia = reader.IsDBNull(reader.GetOrdinal("Familia")) ? "" : reader.GetString(reader.GetOrdinal("Familia")),
                            GrupoGenetico = reader.IsDBNull(reader.GetOrdinal("GrupoGenetico")) ? "" : reader.GetString(reader.GetOrdinal("GrupoGenetico")),
                            TiempoCosecha = reader.IsDBNull(reader.GetOrdinal("TiempoCosecha")) ? "" : reader.GetString(reader.GetOrdinal("TiempoCosecha")),
                            Maduracion = reader.IsDBNull(reader.GetOrdinal("Maduracion")) ? "" : reader.GetString(reader.GetOrdinal("Maduracion")),
                            NotasNutricion = reader.IsDBNull(reader.GetOrdinal("NotasNutricion")) ? "" : reader.GetString(reader.GetOrdinal("NotasNutricion")),
                            DensidadSiembra = reader.IsDBNull(reader.GetOrdinal("DensidadSiembra")) ? "" : reader.GetString(reader.GetOrdinal("DensidadSiembra"))
                        });
                    }
                }
            }

            return variedades;
        }

        public Variedad? ObtenerFicha(int id)
        {
            Variedad? variedad = null;

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                var query = @"SELECT Id, NombreComun, NombreCientifico, Descripcion, 
                                     PorteId, GrainSizeId, AltitudOptimaM, YieldPotentialId, 
                                     QualityLevelId, Obtentor, Familia, GrupoGenetico, 
                                     TiempoCosecha, Maduracion, NotasNutricion, DensidadSiembra
                              FROM Variedades
                              WHERE Id = @Id";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            variedad = new Variedad
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                NombreComun = reader.GetString(reader.GetOrdinal("NombreComun")),
                                NombreCientifico = reader.GetString(reader.GetOrdinal("NombreCientifico")),
                                Descripcion = reader.IsDBNull(reader.GetOrdinal("Descripcion")) ? "" : reader.GetString(reader.GetOrdinal("Descripcion")),
                                Porte = reader.IsDBNull(reader.GetOrdinal("PorteId")) ? "" : reader.GetString(reader.GetOrdinal("PorteId")),
                                TamanoGrano = reader.IsDBNull(reader.GetOrdinal("GrainSizeId")) ? "" : reader.GetString(reader.GetOrdinal("GrainSizeId")),
                                AltitudOptimaM = reader.IsDBNull(reader.GetOrdinal("AltitudOptimaM")) ? 0 : reader.GetInt32(reader.GetOrdinal("AltitudOptimaM")),
                                PotencialRendimiento = reader.IsDBNull(reader.GetOrdinal("YieldPotentialId")) ? "" : reader.GetString(reader.GetOrdinal("YieldPotentialId")),
                                NivelCalidad = reader.IsDBNull(reader.GetOrdinal("QualityLevelId")) ? 0 : reader.GetInt32(reader.GetOrdinal("QualityLevelId")),
                                Obtentor = reader.IsDBNull(reader.GetOrdinal("Obtentor")) ? "" : reader.GetString(reader.GetOrdinal("Obtentor")),
                                Familia = reader.IsDBNull(reader.GetOrdinal("Familia")) ? "" : reader.GetString(reader.GetOrdinal("Familia")),
                                GrupoGenetico = reader.IsDBNull(reader.GetOrdinal("GrupoGenetico")) ? "" : reader.GetString(reader.GetOrdinal("GrupoGenetico")),
                                TiempoCosecha = reader.IsDBNull(reader.GetOrdinal("TiempoCosecha")) ? "" : reader.GetString(reader.GetOrdinal("TiempoCosecha")),
                                Maduracion = reader.IsDBNull(reader.GetOrdinal("Maduracion")) ? "" : reader.GetString(reader.GetOrdinal("Maduracion")),
                                NotasNutricion = reader.IsDBNull(reader.GetOrdinal("NotasNutricion")) ? "" : reader.GetString(reader.GetOrdinal("NotasNutricion")),
                                DensidadSiembra = reader.IsDBNull(reader.GetOrdinal("DensidadSiembra")) ? "" : reader.GetString(reader.GetOrdinal("DensidadSiembra"))
                            };
                        }
                    }
                }
            }

            return variedad;
        }
    }
}
