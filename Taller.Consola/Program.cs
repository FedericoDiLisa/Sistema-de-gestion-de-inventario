using Taller.Consola.Models;

Console.WriteLine("=== PRUEBA DE SISTEMA DE TALLER ===");

// Instanciamos (creamos en memoria) dos objetos a partir de nuestro plano (la clase Insumo)
Insumo rolloPla = new Insumo("Filamento PLA Negro 1kg", "Impresión 3D", 18500.50m, 10, 3);
Insumo termica = new Insumo("Llave Térmica Bipolar 20A", "Electricidad", 9200.00m, 2, 5);

// Usamos sus métodos
rolloPla.DescontarStock(2);

Console.WriteLine($"Insumo: {rolloPla.Nombre} | Stock restante: {rolloPla.StockActual} | ¿Falta stock?: {rolloPla.RequiereReposicion()}");
Console.WriteLine($"Insumo: {termica.Nombre} | Stock restante: {termica.StockActual} | ¿Falta stock?: {termica.RequiereReposicion()}");