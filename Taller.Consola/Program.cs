using Taller.Consola.Models;
using Taller.Consola.Services;

Console.WriteLine("=== SISTEMA DE GESTIÓN DE TALLER ===\n");

// 1. Instanciamos el servicio (el cerebro del inventario)
InventarioService inventario = new InventarioService();

// 2. Cargamos varios insumos al servicio
inventario.RegistrarInsumo(new Insumo("Filamento PLA Negro 1kg", "Impresión 3D", 18500.50m, 10, 3));
inventario.RegistrarInsumo(new Insumo("Llave Térmica Bipolar 20A", "Electricidad", 9200.00m, 2, 5));
inventario.RegistrarInsumo(new Insumo("Cable Unipolar 2.5mm (Rollo)", "Electricidad", 45000.00m, 1, 2));
inventario.RegistrarInsumo(new Insumo("Boquilla Latón 0.4mm", "Impresión 3D", 2500.00m, 15, 5));

// 3. Mostramos el dinero total en mercadería
decimal totalPlata = inventario.CalcularValorTotalInventario();
Console.WriteLine($"Valor total del inventario actual: ${totalPlata:N2}\n");

// 4. Listamos solo los que están en alerta de stock bajo
Console.WriteLine("--- ALERTA: INSUMOS A REPONER ---");
List<Insumo> criticos = inventario.ObtenerCriticosParaReponer();

foreach (Insumo item in criticos)
{
    Console.WriteLine($"- {item.Nombre} ({item.Categoria}) | Stock: {item.StockActual} (Mínimo: {item.StockMinimo})");
}