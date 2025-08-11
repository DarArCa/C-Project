DROP DATABASE IF EXISTS COLCOFF;
CREATE DATABASE COLCOFF CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE COLCOFF;

-- Roles / Usuarios
CREATE TABLE IF NOT EXISTS roles (
  id INT AUTO_INCREMENT PRIMARY KEY,
  nombre VARCHAR(50) NOT NULL UNIQUE
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS usuarios (
  id INT AUTO_INCREMENT PRIMARY KEY,
  nombre_usuario VARCHAR(80) NOT NULL UNIQUE,
  correo VARCHAR(150) NOT NULL UNIQUE,
  contrasena_hash VARCHAR(255) NOT NULL,
  role_id INT NOT NULL,
  esta_activo TINYINT(1) DEFAULT 1,
  creado_en DATETIME DEFAULT CURRENT_TIMESTAMP,
  actualizado_en DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  FOREIGN KEY (role_id) REFERENCES roles(id)
) ENGINE=InnoDB;

-- Lookups (catálogos)
CREATE TABLE IF NOT EXISTS porte (
  id INT AUTO_INCREMENT PRIMARY KEY,
  nombre VARCHAR(50) NOT NULL UNIQUE
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS tamano_grano (
  id INT AUTO_INCREMENT PRIMARY KEY,
  nombre VARCHAR(50) NOT NULL UNIQUE
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS potencial_rendimiento (
  id INT AUTO_INCREMENT PRIMARY KEY,
  nombre VARCHAR(80) NOT NULL UNIQUE
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS nivel_calidad (
  id INT AUTO_INCREMENT PRIMARY KEY,
  nivel TINYINT NOT NULL UNIQUE   -- 1..5
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS tipo_resistencia (
  id INT AUTO_INCREMENT PRIMARY KEY,
  nombre VARCHAR(80) NOT NULL UNIQUE
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS nivel_resistencia (
  id INT AUTO_INCREMENT PRIMARY KEY,
  nombre VARCHAR(50) NOT NULL UNIQUE -- Susceptible, Tolerante, Resistente
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS etiquetas (
  id INT AUTO_INCREMENT PRIMARY KEY,
  nombre VARCHAR(80) NOT NULL UNIQUE
) ENGINE=InnoDB;

-- Variedades
CREATE TABLE IF NOT EXISTS variedades (
  id INT AUTO_INCREMENT PRIMARY KEY,
  nombre_comun VARCHAR(150) NOT NULL,
  nombre_cientifico VARCHAR(200),
  descripcion TEXT,
  porte_id INT,
  grain_size_id INT,
  altitud_optima_m INT,
  yield_potential_id INT,
  quality_level_id INT,
  obtentor VARCHAR(150),
  familia VARCHAR(150),
  grupo_genetico VARCHAR(150),
  tiempo_cosecha VARCHAR(150),
  maduracion VARCHAR(150),
  notas_nutricion TEXT,
  densidad_siembra VARCHAR(150),
  creado_por INT,
  creado_en DATETIME DEFAULT CURRENT_TIMESTAMP,
  actualizado_por INT,
  actualizado_en DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  FOREIGN KEY (porte_id) REFERENCES porte(id),
  FOREIGN KEY (grain_size_id) REFERENCES tamano_grano(id),
  FOREIGN KEY (yield_potential_id) REFERENCES potencial_rendimiento(id),
  FOREIGN KEY (quality_level_id) REFERENCES nivel_calidad(id),
  FOREIGN KEY (creado_por) REFERENCES usuarios(id),
  FOREIGN KEY (actualizado_por) REFERENCES usuarios(id)
) ENGINE=InnoDB;

-- Imágenes (varias por variedad)
CREATE TABLE IF NOT EXISTS imagenes_variedad (
  id INT AUTO_INCREMENT PRIMARY KEY,
  variedad_id INT NOT NULL,
  ruta_archivo VARCHAR(500) NOT NULL,
  texto_alternativo VARCHAR(250),
  leyenda VARCHAR(250),
  es_principal TINYINT(1) DEFAULT 0,
  creado_en DATETIME DEFAULT CURRENT_TIMESTAMP,
  FOREIGN KEY (variedad_id) REFERENCES variedades(id) ON DELETE CASCADE
) ENGINE=InnoDB;

-- Resistencias (N:M con nivel)
CREATE TABLE IF NOT EXISTS resistencias_variedad (
  id INT AUTO_INCREMENT PRIMARY KEY,
  variedad_id INT NOT NULL,
  tipo_resistencia_id INT NOT NULL,
  nivel_resistencia_id INT NOT NULL,
  notas VARCHAR(255),
  FOREIGN KEY (variedad_id) REFERENCES variedades(id) ON DELETE CASCADE,
  FOREIGN KEY (tipo_resistencia_id) REFERENCES tipo_resistencia(id),
  FOREIGN KEY (nivel_resistencia_id) REFERENCES nivel_resistencia(id),
  UNIQUE (variedad_id, tipo_resistencia_id)
) ENGINE=InnoDB;

-- Tags (N:M)
CREATE TABLE IF NOT EXISTS etiquetas_variedad (
  variedad_id INT NOT NULL,
  etiqueta_id INT NOT NULL,
  PRIMARY KEY (variedad_id, etiqueta_id),
  FOREIGN KEY (variedad_id) REFERENCES variedades(id) ON DELETE CASCADE,
  FOREIGN KEY (etiqueta_id) REFERENCES etiquetas(id)
) ENGINE=InnoDB;

-- Catálogos PDF
CREATE TABLE IF NOT EXISTS catalogos_pdf (
  id INT AUTO_INCREMENT PRIMARY KEY,
  usuario_id INT NOT NULL,
  titulo VARCHAR(200),
  ruta_archivo VARCHAR(500),
  criterios_filtro JSON,
  total_items INT DEFAULT 0,
  creado_en DATETIME DEFAULT CURRENT_TIMESTAMP,
  FOREIGN KEY (usuario_id) REFERENCES usuarios(id)
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS items_catalogo_pdf (
  catalogo_id INT NOT NULL,
  variedad_id INT NOT NULL,
  orden INT DEFAULT 0,
  PRIMARY KEY (catalogo_id, variedad_id),
  FOREIGN KEY (catalogo_id) REFERENCES catalogos_pdf(id) ON DELETE CASCADE,
  FOREIGN KEY (variedad_id) REFERENCES variedades(id)
) ENGINE=InnoDB;

-- Historial/auditoría de cambios en variedades
CREATE TABLE IF NOT EXISTS historial_variedad (
  id INT AUTO_INCREMENT PRIMARY KEY,
  variedad_id INT NOT NULL,
  cambiado_por INT,
  tipo_cambio VARCHAR(20),
  cambiado_en DATETIME DEFAULT CURRENT_TIMESTAMP,
  diferencia JSON,
  FOREIGN KEY (variedad_id) REFERENCES variedades(id),
  FOREIGN KEY (cambiado_por) REFERENCES usuarios(id)
) ENGINE=InnoDB;

-- Índices recomendados para filtros
CREATE INDEX IF NOT EXISTS idx_varieties_porte ON variedades(porte_id);
CREATE INDEX IF NOT EXISTS idx_varieties_grain ON variedades(grain_size_id);
CREATE INDEX IF NOT EXISTS idx_varieties_altitude ON variedades(altitud_optima_m);
CREATE INDEX IF NOT EXISTS idx_varieties_yield ON variedades(yield_potential_id);
CREATE INDEX IF NOT EXISTS idx_varieties_quality ON variedades(quality_level_id);

-- Fulltext para búsquedas sobre nombre/descripcion (InnoDB soporta FULLTEXT en MySQL moderno)
ALTER TABLE variedades ADD FULLTEXT INDEX IF NOT EXISTS ft_varieties_name_desc (nombre_comun, nombre_cientifico, descripcion);

-- Seed data útil (roles, resistances, niveles, porte, tamano_grano, quality, yield)
INSERT INTO roles (nombre) VALUES ('admin'), ('editor'), ('viewer')
  ON DUPLICATE KEY UPDATE nombre = VALUES(nombre);

INSERT INTO nivel_resistencia (nombre) VALUES ('Susceptible'), ('Tolerante'), ('Resistente')
  ON DUPLICATE KEY UPDATE nombre = VALUES(nombre);

INSERT INTO tipo_resistencia (nombre) VALUES ('Roya'), ('Antracnosis'), ('Nematodos'), ('Broca'), ('Tizón')
  ON DUPLICATE KEY UPDATE nombre = VALUES(nombre);

INSERT INTO porte (nombre) VALUES ('Alto'), ('Medio'), ('Bajo')
  ON DUPLICATE KEY UPDATE nombre = VALUES(nombre);

INSERT INTO tamano_grano (nombre) VALUES ('Pequeño'), ('Medio'), ('Grande')
  ON DUPLICATE KEY UPDATE nombre = VALUES(nombre);

INSERT INTO potencial_rendimiento (nombre) VALUES ('Muy bajo'), ('Bajo'), ('Medio'), ('Alto'), ('Excepcional')
  ON DUPLICATE KEY UPDATE nombre = VALUES(nombre);

INSERT INTO nivel_calidad (nivel) VALUES (1),(2),(3),(4),(5)
  ON DUPLICATE KEY UPDATE nivel = VALUES(nivel);

-- Ejemplo de tag
INSERT INTO etiquetas (nombre) VALUES ('Tolerante a sequía'), ('Producción alta'), ('Café de especialidad')
  ON DUPLICATE KEY UPDATE nombre = VALUES(nombre);

-- Fin del archivo
