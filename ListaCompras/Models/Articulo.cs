using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;

namespace ListaCompras.Models
{
    public partial class Articulo : ObservableObject
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [ObservableProperty]
        private string nombre = string.Empty;

        [ObservableProperty]
        private int cantidad;

        [ObservableProperty]
        private bool comprado;

        [ObservableProperty]
        private string notas = string.Empty;

        [ObservableProperty]
        private string categoria = string.Empty;

        // Ruta local de la imagen seleccionada (archivo en el dispositivo)
        [ObservableProperty]
        private string imagenPath = string.Empty;
    }
}
