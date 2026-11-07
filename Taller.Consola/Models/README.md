# Capa de Modelos (Models / Entidades)

**¿Qué se guarda acá?**
Clases puras de C# que representan los objetos del mundo real y el reflejo de las tablas de la base de datos SQL (por ejemplo: `Insumo.cs`, `Cliente.cs`, `OrdenTrabajo.cs`).

**Reglas de esta carpeta:**
- Solo contiene propiedades (`get; set;`), constructores y validaciones básicas de los datos propios del objeto (Encapsulamiento).
- **NO** se conecta a la base de datos.
- **NO** tiene `Console.WriteLine` ni pide datos por pantalla.