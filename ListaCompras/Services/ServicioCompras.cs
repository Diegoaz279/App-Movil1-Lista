using System.Collections.Generic;
using System.Threading.Tasks;
using ListaCompras.Models;

namespace ListaCompras.Services
{
    public class ServicioCompras
    {
        private readonly AppDatabase _db;

        public ServicioCompras(AppDatabase db)
        {
            _db = db;
        }

        // === MÉTODOS QUE ESTÁ PIDIENDO TU VIEWMODEL ===

        // Cargar todos los artículos desde SQLite
        public Task<List<Articulo>> ObtenerTodosAsync()
            => _db.GetArticulosAsync();

        // Insertar o actualizar un artículo
        public Task GuardarAsync(Articulo articulo)
            => _db.SaveArticuloAsync(articulo);

        // Eliminar un artículo
        public Task EliminarAsync(Articulo articulo)
            => _db.DeleteArticuloAsync(articulo);

        // Vaciar tabla completa
        public Task VaciarAsync()
            => _db.DeleteAllAsync();
    }
}
