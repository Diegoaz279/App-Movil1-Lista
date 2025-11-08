using ListaCompras.Models;

namespace ListaCompras.Services;

public class ServicioCompras
{
    public List<Articulo> ObtenerEjemplo()
    {
        return new()
        {
            new Articulo { Nombre = "Leche",   Cantidad = 2 },
            new Articulo { Nombre = "Huevos",  Cantidad = 12 },
            new Articulo { Nombre = "Pan",     Cantidad = 1 },
            new Articulo { Nombre = "Tomates", Cantidad = 4 }
        };
    }
}
