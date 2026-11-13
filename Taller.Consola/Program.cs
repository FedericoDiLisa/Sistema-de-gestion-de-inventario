using Taller.Consola.Services;
using Taller.Consola.UI;

InventarioService servicio = new InventarioService();
ConsolaUI interfaz = new ConsolaUI(servicio);

interfaz.Iniciar();