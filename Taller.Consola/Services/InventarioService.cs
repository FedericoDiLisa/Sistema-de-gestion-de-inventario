using Taller.Consola.Models;
using Taller.Consola.Repositories;

namespace Taller.Consola.Services;

public class InventarioService
{
    private readonly InsumoRepository _repository = new InsumoRepository();

    public void RegistrarInsumo(Insumo nuevoInsumo)
    {
        List<Insumo> actuales = _repository.ObtenerTodos();
        bool existe = actuales.Any(i => i.Nombre.Equals(nuevoInsumo.Nombre, StringComparison.OrdinalIgnoreCase));
        
        if (existe)
            throw new InvalidOperationException($"Ya existe el insumo '{nuevoInsumo.Nombre}' en la base de datos.");

        _repository.Insertar(nuevoInsumo);
    }

    public List<Insumo> ObtenerTodos()
    {
        return _repository.ObtenerTodos();
    }

    public List<Insumo> ObtenerCriticosParaReponer()
    {
        return _repository.ObtenerTodos()
                          .Where(i => i.RequiereReposicion())
                          .ToList();
    }

    public decimal CalcularValorTotalInventario()
    {
        return _repository.ObtenerTodos()
                          .Sum(i => i.PrecioUnitario * i.StockActual);
    }
}