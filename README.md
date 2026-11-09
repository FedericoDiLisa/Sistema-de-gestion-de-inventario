# Sistema de Gestión de Inventario para Taller 🛠️

Aplicación de consola desarrollada en **C# (.NET 10)** con persistencia en **SQL Server**, diseñada bajo una **arquitectura en capas** para gestionar el control de stock, costos y alertas de reposición de insumos técnicos (impresión 3D, electricidad y mecánica).

## 🚀 Tecnologías y Conceptos Aplicados

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

## ⚙️ Cómo ejecutar el proyecto localmente

1. Clonar el repositorio:
   ```bash
   git clone [https://github.com/FedericoDiLisa/SistemaGestionTaller.git](https://github.com/FedericoDiLisa/SistemaGestionTaller.git)