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
    Console.WriteLine("4. Registrar entrada / salida de stock (UPDATE)");
    Console.WriteLine("5. Eliminar un insumo por ID (DELETE)");
    Console.WriteLine("6. Buscar insumos por categoría");
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
            case "4":
                GestionarMovimientoStock(inventario);
                break;
            case "5":
                BorrarInsumo(inventario);
                break;
            case "6":
                BuscarPorCategoria(inventario);
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
        Console.WriteLine($"\n[ERROR]: {ex.Message}");
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

static void GestionarMovimientoStock(InventarioService servicio)
{
    Console.WriteLine("\n--- MOVIMIENTO DE STOCK ---");
    Console.Write("Ingrese el ID del insumo: ");
    if (!int.TryParse(Console.ReadLine(), out int id))
        throw new ArgumentException("El ID debe ser un número entero.");

    Console.Write("¿Es ingreso de mercadería (I) o salida/consumo (S)?: ");
    string tipo = (Console.ReadLine() ?? "").Trim().ToUpper();

    if (tipo != "I" && tipo != "S")
        throw new ArgumentException("Debe ingresar 'I' para ingreso o 'S' para salida.");

    Console.Write("Cantidad de unidades: ");
    if (!int.TryParse(Console.ReadLine(), out int cantidad))
        throw new ArgumentException("La cantidad debe ser un número entero.");

    bool esIngreso = (tipo == "I");
    servicio.ModificarStock(id, cantidad, esIngreso);

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("\n¡Stock actualizado correctamente en SQL Server!");
    Console.ResetColor();
}

static void BorrarInsumo(InventarioService servicio)
{
    Console.WriteLine("\n--- ELIMINAR INSUMO ---");
    Console.Write("Ingrese el ID del insumo a eliminar: ");
    if (!int.TryParse(Console.ReadLine(), out int id))
        throw new ArgumentException("El ID debe ser un número entero.");

    servicio.EliminarInsumo(id);

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"\n¡Insumo con ID {id} eliminado de SQL Server!");
    Console.ResetColor();
}

static void BuscarPorCategoria(InventarioService servicio)
{
    Console.WriteLine("\n--- BUSQUEDA POR CATEGORIA ---");
    Console.Write("Ingrese la categoría a buscar: ");
    string categoriaBuscada = Console.ReadLine() ?? "";

    // 1. Guardamos la lista que nos devuelve el servicio
    List<Insumo> resultados = servicio.ObtenerPorCategoria(categoriaBuscada);

    // 2. Validamos si encontró algo o vino vacía
    if (resultados.Count == 0)
    {
        Console.WriteLine($"No se encontraron insumos en la categoría '{categoriaBuscada}'.");
        return;
    }

    Console.WriteLine($"\n--- INSUMOS EN LA CATEGORÍA: {categoriaBuscada} ---");

    // 3. El foreach para recorrer y mostrar cada insumo encontrado
    foreach (Insumo i in resultados)
    {
        Console.WriteLine($"[ID: {i.Id}] {i.Nombre} | Precio: ${i.PrecioUnitario:N2} | Stock: {i.StockActual} (Mín: {i.StockMinimo})");
    }
}