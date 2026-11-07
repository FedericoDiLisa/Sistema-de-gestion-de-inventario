using Taller.Consola.Models;

namespace Taller.Consola.Services;

public class InventarioService
{
    // Lista privada en memoria que guarda todos nuestros objetos Insumo
    private readonly List<Insumo> _insumos = new List<Insumo>();

    // Método para dar de alta un insumo en nuestra lista
    public void RegistrarInsumo(Insumo nuevoInsumo)
    {
        // Validación de negocio: no permitir dos insumos con el mismo nombre exacto
        bool existe = _insumos.Any(i => i.Nombre.Equals(nuevoInsumo.Nombre, StringComparison.OrdinalIgnoreCase));
        
        if (existe)
            throw new InvalidOperationException($"Ya existe un insumo registrado con el nombre '{nuevoInsumo.Nombre}'.");

        _insumos.Add(nuevoInsumo);
    }

    // Devuelve todos los insumos
    public List<Insumo> ObtenerTodos()
    {
        return _insumos;
    }

    // Usa LINQ (.Where) para filtrar igual que un WHERE de SQL
    public List<Insumo> ObtenerCriticosParaReponer()
    {
        return _insumos.Where(i => i.RequiereReposicion()).ToList();
    }

    // Calcula cuánta plata tenés parada en mercadería (Precio * Stock de cada uno)
    public decimal CalcularValorTotalInventario()
    {
        return _insumos.Sum(i => i.PrecioUnitario * i.StockActual);
    }
}