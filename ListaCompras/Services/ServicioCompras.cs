using ListaCompras.Models;

namespace ListaCompras.Services
{
    // Clase que actúa como "servicio" para manejar la lógica relacionada con las compras o artículos
    public class ServicioCompras
    {
        // Método que devuelve una lista de ejemplo de artículos de compra
        // Se usa al iniciar la aplicación para mostrar datos predefinidos en la lista
        public List<Articulo> ObtenerEjemplo()
        {
            // Retorna una lista inicial con algunos productos de ejemplo
            // Esto simula datos que podrían venir de una base de datos o API
            return new()
            {
                new Articulo { Nombre = "Leche",   Cantidad = 2 },
                new Articulo { Nombre = "Huevos",  Cantidad = 12 },
                new Articulo { Nombre = "Pan",     Cantidad = 1 },
                new Articulo { Nombre = "Tomates", Cantidad = 4 }
            };
        }
    }
}
