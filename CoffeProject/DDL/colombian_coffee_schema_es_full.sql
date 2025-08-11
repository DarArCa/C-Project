DROP DATABASE IF EXISTS COLCOFF;
CREATE DATABASE COLCOFF CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE COLCOFF;

-- Roles / Usuarios
CREATE TABLE IF NOT EXISTS Roles (
  Id INT AUTO_INCREMENT PRIMARY KEY,
  Nombre VARCHAR(50) NOT NULL UNIQUE
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS Usuarios (
  Id INT AUTO_INCREMENT PRIMARY KEY,
  NombreUsuario VARCHAR(80) NOT NULL UNIQUE,
  Correo VARCHAR(150) NOT NULL UNIQUE,
  ContrasenaHash VARCHAR(255) NOT NULL,
  RoleId INT NOT NULL,
  EstaActivo TINYINT(1) DEFAULT 1,
  CreadoEn DATETIME DEFAULT CURRENT_TIMESTAMP,
  ActualizadoEn DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  FOREIGN KEY (RoleId) REFERENCES Roles(Id)
) ENGINE=InnoDB;

-- Lookups (catálogos)
CREATE TABLE IF NOT EXISTS Porte (
  Id INT AUTO_INCREMENT PRIMARY KEY,
  Nombre VARCHAR(50) NOT NULL UNIQUE
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS TamanoGrano (
  Id INT AUTO_INCREMENT PRIMARY KEY,
  Nombre VARCHAR(50) NOT NULL UNIQUE
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS PotencialRendimiento (
  Id INT AUTO_INCREMENT PRIMARY KEY,
  Nombre VARCHAR(80) NOT NULL UNIQUE
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS NivelCalidad (
  Id INT AUTO_INCREMENT PRIMARY KEY,
  Nivel TINYINT NOT NULL UNIQUE
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS TipoResistencia (
  Id INT AUTO_INCREMENT PRIMARY KEY,
  Nombre VARCHAR(80) NOT NULL UNIQUE
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS NivelResistencia (
  Id INT AUTO_INCREMENT PRIMARY KEY,
  Nombre VARCHAR(50) NOT NULL UNIQUE
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS Etiquetas (
  Id INT AUTO_INCREMENT PRIMARY KEY,
  Nombre VARCHAR(80) NOT NULL UNIQUE
) ENGINE=InnoDB;

-- Variedades
CREATE TABLE IF NOT EXISTS Variedades (
  Id INT AUTO_INCREMENT PRIMARY KEY,
  NombreComun VARCHAR(150) NOT NULL,
  NombreCientifico VARCHAR(200),
  Descripcion TEXT,
  PorteId INT,
  GrainSizeId INT,
  AltitudOptimaM INT,
  YieldPotentialId INT,
  QualityLevelId INT,
  Obtentor VARCHAR(150),
  Familia VARCHAR(150),
  GrupoGenetico VARCHAR(150),
  TiempoCosecha VARCHAR(150),
  Maduracion VARCHAR(150),
  NotasNutricion TEXT,
  DensidadSiembra VARCHAR(150),
  CreadoPor INT,
  CreadoEn DATETIME DEFAULT CURRENT_TIMESTAMP,
  ActualizadoPor INT,
  ActualizadoEn DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  FOREIGN KEY (PorteId) REFERENCES Porte(Id),
  FOREIGN KEY (GrainSizeId) REFERENCES TamanoGrano(Id),
  FOREIGN KEY (YieldPotentialId) REFERENCES PotencialRendimiento(Id),
  FOREIGN KEY (QualityLevelId) REFERENCES NivelCalidad(Id),
  FOREIGN KEY (CreadoPor) REFERENCES Usuarios(Id),
  FOREIGN KEY (ActualizadoPor) REFERENCES Usuarios(Id)
) ENGINE=InnoDB;

-- Imágenes (varias por variedad)
CREATE TABLE IF NOT EXISTS ImagenesVariedad (
  Id INT AUTO_INCREMENT PRIMARY KEY,
  VariedadId INT NOT NULL,
  RutaArchivo VARCHAR(500) NOT NULL,
  TextoAlternativo VARCHAR(250),
  Leyenda VARCHAR(250),
  EsPrincipal TINYINT(1) DEFAULT 0,
  CreadoEn DATETIME DEFAULT CURRENT_TIMESTAMP,
  FOREIGN KEY (VariedadId) REFERENCES Variedades(Id) ON DELETE CASCADE
) ENGINE=InnoDB;

-- Resistencias (N:M con nivel)
CREATE TABLE IF NOT EXISTS ResistenciasVariedad (
  Id INT AUTO_INCREMENT PRIMARY KEY,
  VariedadId INT NOT NULL,
  TipoResistenciaId INT NOT NULL,
  NivelResistenciaId INT NOT NULL,
  Notas VARCHAR(255),
  FOREIGN KEY (VariedadId) REFERENCES Variedades(Id) ON DELETE CASCADE,
  FOREIGN KEY (TipoResistenciaId) REFERENCES TipoResistencia(Id),
  FOREIGN KEY (NivelResistenciaId) REFERENCES NivelResistencia(Id),
  UNIQUE (VariedadId, TipoResistenciaId)
) ENGINE=InnoDB;

-- Tags (N:M)
CREATE TABLE IF NOT EXISTS EtiquetasVariedad (
  VariedadId INT NOT NULL,
  EtiquetaId INT NOT NULL,
  PRIMARY KEY (VariedadId, EtiquetaId),
  FOREIGN KEY (VariedadId) REFERENCES Variedades(Id) ON DELETE CASCADE,
  FOREIGN KEY (EtiquetaId) REFERENCES Etiquetas(Id)
) ENGINE=InnoDB;

-- Catálogos PDF
CREATE TABLE IF NOT EXISTS CatalogosPdf (
  Id INT AUTO_INCREMENT PRIMARY KEY,
  UsuarioId INT NOT NULL,
  Titulo VARCHAR(200),
  RutaArchivo VARCHAR(500),
  CriteriosFiltro JSON,
  TotalItems INT DEFAULT 0,
  CreadoEn DATETIME DEFAULT CURRENT_TIMESTAMP,
  FOREIGN KEY (UsuarioId) REFERENCES Usuarios(Id)
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS ItemsCatalogoPdf (
  CatalogoId INT NOT NULL,
  VariedadId INT NOT NULL,
  Orden INT DEFAULT 0,
  PRIMARY KEY (CatalogoId, VariedadId),
  FOREIGN KEY (CatalogoId) REFERENCES CatalogosPdf(Id) ON DELETE CASCADE,
  FOREIGN KEY (VariedadId) REFERENCES Variedades(Id)
) ENGINE=InnoDB;

-- Historial/auditoría de cambios en variedades
CREATE TABLE IF NOT EXISTS HistorialVariedad (
  Id INT AUTO_INCREMENT PRIMARY KEY,
  VariedadId INT NOT NULL,
  CambiadoPor INT,
  TipoCambio VARCHAR(20),
  CambiadoEn DATETIME DEFAULT CURRENT_TIMESTAMP,
  Diferencia JSON,
  FOREIGN KEY (VariedadId) REFERENCES Variedades(Id),
  FOREIGN KEY (CambiadoPor) REFERENCES Usuarios(Id)
) ENGINE=InnoDB;

-- Índices recomendados para filtros
CREATE INDEX idx_varieties_porte ON Variedades(PorteId);
CREATE INDEX idx_varieties_grain ON Variedades(GrainSizeId);
CREATE INDEX idx_varieties_altitude ON Variedades(AltitudOptimaM);
CREATE INDEX idx_varieties_yield ON Variedades(YieldPotentialId);
CREATE INDEX idx_varieties_quality ON Variedades(QualityLevelId);

-- Fulltext
ALTER TABLE Variedades ADD FULLTEXT INDEX ft_varieties_name_desc (NombreComun, NombreCientifico, Descripcion);

-- Seed data
INSERT INTO Roles (Nombre) VALUES ('admin'), ('editor'), ('viewer')
  ON DUPLICATE KEY UPDATE Nombre = VALUES(Nombre);

INSERT INTO NivelResistencia (Nombre) VALUES ('Susceptible'), ('Tolerante'), ('Resistente')
  ON DUPLICATE KEY UPDATE Nombre = VALUES(Nombre);

INSERT INTO TipoResistencia (Nombre) VALUES ('Roya'), ('Antracnosis'), ('Nematodos'), ('Broca'), ('Tizón')
  ON DUPLICATE KEY UPDATE Nombre = VALUES(Nombre);

INSERT INTO Porte (Nombre) VALUES ('Alto'), ('Medio'), ('Bajo')
  ON DUPLICATE KEY UPDATE Nombre = VALUES(Nombre);

INSERT INTO TamanoGrano (Nombre) VALUES ('Pequeño'), ('Medio'), ('Grande')
  ON DUPLICATE KEY UPDATE Nombre = VALUES(Nombre);

INSERT INTO PotencialRendimiento (Nombre) VALUES ('Muy bajo'), ('Bajo'), ('Medio'), ('Alto'), ('Excepcional')
  ON DUPLICATE KEY UPDATE Nombre = VALUES(Nombre);

INSERT INTO NivelCalidad (Nivel) VALUES (1),(2),(3),(4),(5)
  ON DUPLICATE KEY UPDATE Nivel = VALUES(Nivel);

INSERT INTO Etiquetas (Nombre) VALUES ('Tolerante a sequía'), ('Producción alta'), ('Café de especialidad')
  ON DUPLICATE KEY UPDATE Nombre = VALUES(Nombre);
