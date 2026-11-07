namespace Taller.Consola.Models;

public class Insumo
{
    // 1. PROPIEDADES (Encapsulamiento)
    // "private set" significa que desde afuera de esta clase NADIE puede cambiar el valor a mano.
    // Solo esta misma clase puede modificar sus datos validando que sean correctos.
    public int Id { get; private set; }
    public string Nombre { get; private set; }
    public string Categoria { get; private set; }
    public decimal PrecioUnitario { get; private set; }
    public int StockActual { get; private set; }
    public int StockMinimo { get; private set; }

    // 2. CONSTRUCTOR
    // Obliga a que, para crear un Insumo en memoria (new Insumo(...)), 
    // sí o sí nos pasen los datos básicos válidos.
    public Insumo(string nombre, string categoria, decimal precioUnitario, int stockActual, int stockMinimo)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre del insumo no puede estar vacío.");

        if (precioUnitario < 0)
            throw new ArgumentException("El precio no puede ser negativo.");

        if (stockActual < 0 || stockMinimo < 0)
            throw new ArgumentException("El stock no puede ser negativo.");

        Nombre = nombre;
        Categoria = categoria;
        PrecioUnitario = precioUnitario;
        StockActual = stockActual;
        StockMinimo = stockMinimo;
    }

    // 3. MÉTODOS DE COMPORTAMIENTO (Lo que el objeto sabe hacer con sus propios datos)
    public void AgregarStock(int cantidad)
    {
        if (cantidad <= 0)
            throw new ArgumentException("La cantidad a agregar debe ser mayor a cero.");

        StockActual += cantidad;
    }

    public void DescontarStock(int cantidad)
    {
        if (cantidad <= 0)
            throw new ArgumentException("La cantidad a descontar debe ser mayor a cero.");

        if (cantidad > StockActual)
            throw new InvalidOperationException($"Stock insuficiente de {Nombre}. Stock actual: {StockActual}.");

        StockActual -= cantidad;
    }

    public bool RequiereReposicion()
    {
        return StockActual <= StockMinimo;
    }
}