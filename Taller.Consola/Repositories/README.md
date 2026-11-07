# Capa de Acceso a Datos (Repositories)

**¿Qué se guarda acá?**
Las clases encargadas exclusivamente de hablar con SQL Server (por ejemplo: `InsumoRepository.cs`).

**Reglas de esta carpeta:**
- Acá viven las cadenas de conexión y las consultas SQL (`SELECT`, `INSERT`, `UPDATE`, `DELETE`).
- Recibe objetos de la carpeta `Models` para guardarlos en la base de datos, o lee filas de SQL y las transforma en objetos (`List<T>`) para devolvérselos al programa.
- **NO** toma decisiones de negocio ni interactúa con el usuario.