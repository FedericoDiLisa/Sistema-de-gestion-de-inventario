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

    public List<Insumo> ObtenerPorCategoria(string categoriaBuscada)
    {
        return _repository.ObtenerTodos()
                        .Where(i => i.Categoria.Equals(categoriaBuscada, StringComparison.OrdinalIgnoreCase))
                        .ToList();
    }

    public List<Insumo> ObtenerOrdenadosPorPrecioDescendente()
    {
        return _repository.ObtenerTodos()
                          .OrderByDescending(i => i.PrecioUnitario)
                          .ToList();
    }

    public decimal CalcularValorTotalInventario()
    {
        return _repository.ObtenerTodos()
                          .Sum(i => i.PrecioUnitario * i.StockActual);
    }

    public void ModificarStock(int idInsumo, int cantidad, bool esIngreso)
    {
        // 1. Buscamos el insumo en la base de datos usando LINQ (.FirstOrDefault)
        Insumo? insumo = _repository.ObtenerTodos().FirstOrDefault(i => i.Id == idInsumo);

        if (insumo == null)
            throw new KeyNotFoundException($"No existe ningún insumo con el ID {idInsumo}.");

        // 2. Usamos los métodos de POO de tu clase Insumo (que validan que no quede stock negativo)
        if (esIngreso)
            insumo.AgregarStock(cantidad);
        else
            insumo.DescontarStock(cantidad);

        // 3. Guardamos el nuevo stock resultante en SQL Server
        _repository.ActualizarStock(insumo.Id, insumo.StockActual);
    }

    public void EliminarInsumo(int idInsumo)
    {
        bool eliminado = _repository.Eliminar(idInsumo);
        if (!eliminado)
            throw new KeyNotFoundException($"No se encontró ningún insumo con el ID {idInsumo} para eliminar.");
    }

    public void ExportarInventarioCsv(string rutaArchivo)
    {
        List<Insumo> insumos = _repository.ObtenerTodos();
        List<string> lineas = new List<string>();

        // 1. Agregamos el encabezado (los títulos de las columnas en Excel)
        lineas.Add("Id;Nombre;Categoria;PrecioUnitario;StockActual;StockMinimo;RequiereReposicion");

        // 2. Recorremos los insumos y armamos un renglón por cada uno separado por ';'
        foreach (Insumo i in insumos)
        {
            string alerta = i.RequiereReposicion() ? "SI" : "NO";
            string fila = $"{i.Id};{i.Nombre};{i.Categoria};{i.PrecioUnitario:F2};{i.StockActual};{i.StockMinimo};{alerta}";
            lineas.Add(fila);
        }

        // 3. Guardamos todas las líneas en el archivo físico (con UTF8 para que se vean bien las tildes en Excel)
        File.WriteAllLines(rutaArchivo, lineas, System.Text.Encoding.UTF8);
    }
}

