using Taller.Consola.Models;
using Taller.Consola.Services;

InventarioService inventario = new InventarioService();
bool continuar = true;

while (continuar)
{
    Console.WriteLine("\n==========================================");
    Console.WriteLine("   SISTEMA DE GESTIÓN DE TALLER (SQL)     ");
    Console.WriteLine("==========================================");
    Console.WriteLine("1. Ver todos los insumos en stock");
    Console.WriteLine("2. Ver alertas de reposición (Stock Crítico)");
    Console.WriteLine("3. Registrar un nuevo insumo");
    Console.WriteLine("0. Salir");
    Console.Write("\nSeleccione una opción: ");

    string? opcion = Console.ReadLine();

    try
    {
        switch (opcion)
        {
            case "1":
                MostrarTodos(inventario);
                break;
            case "2":
                MostrarCriticos(inventario);
                break;
            case "3":
                CargarNuevoInsumo(inventario);
                break;
            case "0":
                continuar = false;
                Console.WriteLine("Cerrando sistema...");
                break;
            default:
                Console.WriteLine("Opción no válida. Intente de nuevo.");
                break;
        }
    }
    catch (Exception ex)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"\n[ERROR DE VALIDACIÓN]: {ex.Message}");
        Console.ResetColor();
    }
}

// --- FUNCIONES AUXILIARES DE PANTALLA ---

static void MostrarTodos(InventarioService servicio)
{
    List<Insumo> lista = servicio.ObtenerTodos();
    Console.WriteLine("\n--- LISTADO COMPLETO DE INSUMOS (DESDE SQL SERVER) ---");
    
    foreach (Insumo i in lista)
    {
        Console.WriteLine($"[ID: {i.Id}] {i.Nombre} | Cat: {i.Categoria} | Precio: ${i.PrecioUnitario:N2} | Stock: {i.StockActual} (Mín: {i.StockMinimo})");
    }

    decimal total = servicio.CalcularValorTotalInventario();
    Console.WriteLine($"\n>> VALOR TOTAL EN MERCADERÍA: ${total:N2}");
}

static void MostrarCriticos(InventarioService servicio)
{
    List<Insumo> criticos = servicio.ObtenerCriticosParaReponer();
    Console.WriteLine("\n--- ALERTA: INSUMOS A REPONER ---");

    if (criticos.Count == 0)
    {
        Console.WriteLine("Todo el stock está en niveles óptimos.");
        return;
    }

    foreach (Insumo i in criticos)
    {
        Console.WriteLine($"- [ID: {i.Id}] {i.Nombre} ({i.Categoria}) | Stock actual: {i.StockActual} (Mínimo requerido: {i.StockMinimo})");
    }
}

static void CargarNuevoInsumo(InventarioService servicio)
{
    Console.WriteLine("\n--- NUEVO INSUMO ---");
    
    Console.Write("Nombre del insumo: ");
    string nombre = Console.ReadLine() ?? "";

    Console.Write("Categoría (ej. Electricidad, Impresión 3D, Mecánica): ");
    string categoria = Console.ReadLine() ?? "";

    Console.Write("Precio Unitario: ");
    if (!decimal.TryParse(Console.ReadLine(), out decimal precio))
        throw new ArgumentException("El precio ingresado no es un número válido.");

    Console.Write("Stock Actual: ");
    if (!int.TryParse(Console.ReadLine(), out int stockActual))
        throw new ArgumentException("El stock actual debe ser un número entero.");

    Console.Write("Stock Mínimo: ");
    if (!int.TryParse(Console.ReadLine(), out int stockMinimo))
        throw new ArgumentException("El stock mínimo debe ser un número entero.");

    Insumo nuevo = new Insumo(nombre, categoria, precio, stockActual, stockMinimo);
    servicio.RegistrarInsumo(nuevo);

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("\n¡Insumo guardado correctamente en SQL Server!");
    Console.ResetColor();
}