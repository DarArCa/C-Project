# C-Project
Usuario con permisos de administrador
usuario : cristian
contraseña : 1234
Usuario con permisos de Cliente
usuario : alex
contraseña : 123

Desarrollo Aplicación de Desktop -
Colombian Coffee ☕
Colombia es reconocida mundialmente por producir uno de los cafés más finos y suaves del
planeta. La historia del café colombiano se remonta al siglo XVIII, cuando fue introducido por
misioneros jesuitas. Desde entonces, ha evolucionado hasta convertirse en uno de los principales
productos de exportación del país, con un ecosistema robusto de pequeños productores que
cultivan en su mayoría variedades arábicas de alta calidad.
La Federación Nacional de Cafeteros de Colombia, fundada en 1927, ha sido clave en la
promoción de prácticas sostenibles, investigación agronómica, y el desarrollo de variedades
adaptadas a los retos climáticos y enfermedades como la roya. Entre las más conocidas están
Típica, Borbón, Maragogipe, Tabi, Caturra y la Variedad Colombia, todas con características
específicas en sabor, altitud, resistencia y productividad.
Contexto y Especificaciones:
El sistema mostrará información detallada de cada variedad y permitirá la filtración por diferentes
atributos agronómicos y resistencias a enfermedades. Además, contará con una sección de
sugerencias personalizadas, un panel administrativo completo. El aplicativo estará
desarrollado en C# utilizando Entity Framework y Mysql , cumpliendo con principios SOLID y
arquitectura de puertos y adaptadore = Vertical Slicing . El frontend será desarrollado en consola.
Propósito del proyecto
El objetivo principal es desarrollar un aplicativo interactivo y funcional, accesible y visualmente
atractivo, que clasifique y muestre en fichas técnicas las principales variedades de café cultivadas
en Colombia, permitiendo búsquedas, filtros y reportes personalizados.
El sistema Colombian Coffee debe permitir:
Visualizar un catálogo completo de variedades de café con fichas técnicas detalladas.
Filtrar por porte, tamaño de grano, altitud, potencial de rendimiento, y resistencia a
enfermedades.
Ver cada variedad con su nombre, imagen, descripción, datos agronómicos, historia, y
grupo genético.
Generar un catálogo PDF descargable con las variedades seleccionadas por el usuario.
https://ironpdf.com/es/blog/using-ironpdf/csharp-create-pdf-tutorial/ , https://www.c-sharpc
orner.com/UploadFile/f4f047/generating-pdf-file-using-C-Sharp/, https://craftmypdf.com/blo
g/5-ways-to-generate-pdfs-with-c-sharp/, https://help.syncfusion.com/document-processing/
pdf/pdf-library/net/create-pdf-file-in-console, https://dev.to/mhamzap10/c-pdf-generator-tut
orial-html-to-pdf-merge-watermark-extract-h3i
Contar con un panel administrativo para gestión CRUD del contenido.
Requisitos funcionales
Módulo de exploración de variedades
Visualización ficha técnica con:
Nombre común y científico.
Imagen de referencia (grano o arbusto).
Descripción general.
Porte (alto o bajo).
Tamaño del grano (pequeño, medio, grande).
Altitud óptima de siembra.
Potencial de rendimiento (de muy bajo a excepcional).
Calidad del grano según altitud (en 5 niveles).
Resistencias a: roya, antracnosis y nematodos (susceptible, tolerante, resistente).
Información agronómica complementaria: tiempo de cosecha, maduración, nutrición,
densidad de siembra.
Historia y linaje genético (obtentor, familia, grupo).
Filtros dinámicos
Filtrar por cualquiera de los atributos anteriores.
Combinaciones de filtros para sugerencias inteligentes basadas en criterios agronómicos.
Panel administrativo
CRUD completo de variedades
Gestión del contenido visual, histórico y técnico.
Autenticación de usuarios.
Generador de Catálogo PDF
Exportación de resultados filtrados a un catálogo PDF personalizado.
Generación directa desde el aplicativo usuando cualquier nuget o libreria soportada.
Funcionalidades del frontend:
Formularios de login y CRUD con validaciones del lado del cliente y del servidor.
Navegación clara e intuitiva entre secciones.
Buscador avanzado con filtros combinables.
Alertas visuales para errores, confirmaciones y sugerencias de uso.
Vista PDF antes de su descarga.
Importancia del frontend:
El frontend en este proyecto es más que una capa estética: es la puerta de entrada al
conocimiento técnico y visual del café colombiano. Su correcto diseño asegura que:
Los usuarios entiendan y valoren la información compleja de forma accesible.
Los agricultores, exportadores y técnicos puedan tomar decisiones a partir de datos claros y
filtros precisos.
La experiencia de navegación sea cómoda y permita futuras extensiones como dashboards
analíticos, comparativas, e incluso simuladores.
Diagramas
Diagrama Entidad-Relación (MER) de variedades y atributos.
Opcional: flujos de navegación del usuario (wireframes o mockups).
Requisitos no funcionales
Uso de sentencias preparadas con LINQ.
Código estructurado con principios SOLID.
Arquitectura de puerto con vertical slicing.
Patrones de diseño implementados (por ejemplo: Repository, Servicios).
Validación de datos en frontend y base datos usando procedimientos almacenados,
funciones, triggers
Interfaz adaptable limpia y accesible.
Herramientas y tecnologías:
Framework: Entity framework.
Frontend: C# (Console application).
Base de datos: MySQL.
PDF: Segun las necesidades y preferencias.
Control de versiones: Git Flow y GitHub.
Sugerencias:
Empieza por diseñar la base de datos relacional con atributos categóricos bien normalizados.
Usa Factory Pattern (EF) para crear instancias de clases DAO o controladores.
Aplica el patrón Singleton para la conexión a la base de datos con PDO.
Documenta biencada funcion del programa.
Recursos:
LA CARTILLA CAFETERA
Variedades de Café del Mundo
https://products.aspose.com/words/es/net/conversion/image-to-jpg/
https://github.com/trainingLeader/loadimg.git (Repositorio ejemplo carga de Imágenes)