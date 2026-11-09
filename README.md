# Sistema de Gestión de Inventario para Taller 🛠️

Aplicación de consola desarrollada en **C# (.NET 10)** con persistencia en **SQL Server**, diseñada bajo una **arquitectura en capas** para gestionar el control de stock, costos y alertas de reposición de insumos técnicos (impresión 3D, electricidad y mecánica).

> 💡 **Nota de transparencia y propósito educativo:**
> Este repositorio fue construido como un **laboratorio práctico de estudio mediante *Pair Programming* asistido por IA**. El objetivo principal de este proyecto es trascender los ejercicios básicos de consola del primer año académico para comprender, desglosar y asimilar cómo se estructuran las bases y buenas prácticas de la industria (separación de responsabilidades en capas, encapsulamiento real en POO, consultas seguras a SQL Server y flujo de trabajo con Git).

## 🚀 Tecnologías y Conceptos en Estudio

* **Lenguaje y Framework:** C# / .NET 10
* **Base de Datos:** SQL Server (`Microsoft.Data.SqlClient` - ADO.NET)
* **Arquitectura en 3 Capas:**
  * `Models`: Entidades de dominio con encapsulamiento estricto (`private set`), sobrecarga de constructores y validaciones de estado.
  * `Repositories`: Capa de acceso a datos con consultas parametrizadas (prevención de *SQL Injection*) y gestión de conexiones mediante bloques `using`.
  * `Services`: Lógica de negocio, reglas de validación y consultas en memoria con **LINQ** (`.Where`, `.Sum`, `.Any`, `.FirstOrDefault`).
* **Manejo de Errores:** Uso de excepciones tipadas (`ArgumentException`, `InvalidOperationException`, `KeyNotFoundException`) capturadas en la capa de presentación (`try/catch`).

## 📋 Funcionalidades (CRUD Completo)

1. **Listado general y valorización:** Consulta en tiempo real a SQL Server y cálculo del capital total invertido en mercadería (`decimal`).
2. **Alertas de stock crítico:** Filtro automático de insumos cuyo stock actual se encuentra en o por debajo del mínimo requerido.
3. **Alta de insumos (`INSERT`):** Registro con validación de duplicados y reglas de negocio.
4. **Movimientos de inventario (`UPDATE`):** Ingreso y egreso de materiales con control de stock negativo.
5. **Baja de insumos (`DELETE`):** Eliminación física por identificador (`Id`).