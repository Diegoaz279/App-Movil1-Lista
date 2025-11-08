using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ListaCompras.Models;
using ListaCompras.Services;

namespace ListaCompras.ViewModels;

public partial class ListaComprasViewModel : ObservableObject
{
    private readonly ServicioCompras _servicio;

    [ObservableProperty] private ObservableCollection<Articulo> articulos = new();

    // Entradas del formulario
    [ObservableProperty] private string nuevoNombre = string.Empty;
    [ObservableProperty] private string nuevaCantidadTexto = string.Empty;
    [ObservableProperty] private string nuevasNotas = string.Empty;

    [ObservableProperty] private string mensajeError = string.Empty;

    public ListaComprasViewModel()
    {
        _servicio = new ServicioCompras();
        Articulos = new ObservableCollection<Articulo>(_servicio.ObtenerEjemplo());
    }

    [RelayCommand]
    private void AgregarArticulo()
    {
        MensajeError = string.Empty;

        if (string.IsNullOrWhiteSpace(NuevoNombre))
        {
            MensajeError = "Escribe el nombre del producto.";
            return;
        }

        int cantidad = 1;
        if (!string.IsNullOrWhiteSpace(NuevaCantidadTexto))
        {
            if (!int.TryParse(NuevaCantidadTexto, out cantidad) || cantidad <= 0)
            {
                MensajeError = "Cantidad inválida. Usa un número mayor que 0.";
                return;
            }
        }

        Articulos.Add(new Articulo
        {
            Nombre = NuevoNombre.Trim(),
            Cantidad = cantidad,
            Notas = NuevasNotas?.Trim() ?? string.Empty,
            Comprado = false
        });

        // Limpiar
        NuevoNombre = string.Empty;
        NuevaCantidadTexto = string.Empty;
        NuevasNotas = string.Empty;
    }

    [RelayCommand]
    private void EliminarArticulo(Articulo? articulo)
    {
        if (articulo is null) return;
        if (Articulos.Contains(articulo))
            Articulos.Remove(articulo);
    }

    [RelayCommand]
    private void VaciarLista()
    {
        Articulos.Clear();
    }
}
