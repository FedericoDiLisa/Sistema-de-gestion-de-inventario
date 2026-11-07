using Microsoft.Data.SqlClient;
using Taller.Consola.Models;

namespace Taller.Consola.Repositories;

public class InsumoRepository
{
    // Cadena de conexión a tu motor local SQLEXPRESS y tu base TallerDB
    private readonly string _connectionString = 
        "Server=localhost\\SQLEXPRESS;Database=TallerDB;Trusted_Connection=True;TrustServerCertificate=True;";

    // 1. GUARDAR EN SQL (INSERT)
    public void Insertar(Insumo insumo)
    {
        using SqlConnection conexion = new SqlConnection(_connectionString);
        conexion.Open();

        string query = @"
            INSERT INTO Insumos (Nombre, Categoria, PrecioUnitario, StockActual, StockMinimo)
            VALUES (@Nombre, @Categoria, @PrecioUnitario, @StockActual, @StockMinimo);";

        using SqlCommand comando = new SqlCommand(query, conexion);
        
        // Usamos parámetros (@) para evitar hackeos por Inyección SQL (Regla de oro en entrevistas)
        comando.Parameters.AddWithValue("@Nombre", insumo.Nombre);
        comando.Parameters.AddWithValue("@Categoria", insumo.Categoria);
        comando.Parameters.AddWithValue("@PrecioUnitario", insumo.PrecioUnitario);
        comando.Parameters.AddWithValue("@StockActual", insumo.StockActual);
        comando.Parameters.AddWithValue("@StockMinimo", insumo.StockMinimo);

        comando.ExecuteNonQuery();
    }

    // 2. LEER TODOS DESDE SQL (SELECT)
    public List<Insumo> ObtenerTodos()
    {
        List<Insumo> lista = new List<Insumo>();

        using SqlConnection conexion = new SqlConnection(_connectionString);
        conexion.Open();

        string query = "SELECT Id, Nombre, Categoria, PrecioUnitario, StockActual, StockMinimo FROM Insumos;";

        using SqlCommand comando = new SqlCommand(query, conexion);
        using SqlDataReader lector = comando.ExecuteReader();

        // Mientras haya filas en la tabla de SQL, las leemos una por una y las convertimos en objetos Insumo
        while (lector.Read())
        {
            Insumo insumo = new Insumo(
                id: lector.GetInt32(0),
                nombre: lector.GetString(1),
                categoria: lector.GetString(2),
                precioUnitario: lector.GetDecimal(3),
                stockActual: lector.GetInt32(4),
                stockMinimo: lector.GetInt32(5)
            );

            lista.Add(insumo);
        }

        return lista;
    }
}