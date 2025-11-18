using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using ListaCompras.Models;
using SQLite;

namespace ListaCompras.Services
{
    public class AppDatabase
    {
        private readonly SQLiteAsyncConnection _db;

        public AppDatabase(string dbPath)
        {
            _db = new SQLiteAsyncConnection(dbPath);
            _db.CreateTableAsync<Articulo>().Wait();
        }

        public Task<List<Articulo>> GetArticulosAsync()
            => _db.Table<Articulo>().ToListAsync();

        public Task<int> SaveArticuloAsync(Articulo articulo)
        {
            if (articulo.Id != 0)
                return _db.UpdateAsync(articulo);

            return _db.InsertAsync(articulo);
        }

        public Task<int> DeleteArticuloAsync(Articulo articulo)
            => _db.DeleteAsync(articulo);

        public Task<int> DeleteAllAsync()
            => _db.DeleteAllAsync<Articulo>();
    }
}
