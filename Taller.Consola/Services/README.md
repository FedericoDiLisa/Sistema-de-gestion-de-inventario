# Capa de Lógica de Negocio (Services)

**¿Qué se guarda acá?**
El "cerebro" del sistema y las reglas del taller (por ejemplo: `StockService.cs`, `PresupuestoService.cs`).

**Reglas de esta carpeta:**
- Coordina todo: recibe lo que pide el menú principal (`Program.cs`), valida las reglas (ej: "no descontar stock si la cantidad solicitada supera lo disponible", "calcular recargo o IVA") y llama al `Repository` correspondiente para guardar en SQL.
- Separa la interfaz de consola de los datos puros.