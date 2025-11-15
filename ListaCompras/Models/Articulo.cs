using CommunityToolkit.Mvvm.ComponentModel;

namespace ListaCompras.Models
{
    // Modelo que representa un artículo en la lista de compras
    public partial class Articulo : ObservableObject
    {
        // Nombre del producto
        [ObservableProperty]
        private string nombre = string.Empty;

        // Cantidad del producto
        [ObservableProperty]
        private int cantidad;

        // Indica si fue comprado
        [ObservableProperty]
        private bool comprado;

        // Notas adicionales
        [ObservableProperty]
        private string notas = string.Empty;
    }
}
