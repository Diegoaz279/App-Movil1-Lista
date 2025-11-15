using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ListaCompras.Models;
using ListaCompras.Services;

namespace ListaCompras.ViewModels;

// ViewModel principal de la lista de compras
public partial class ListaComprasViewModel : ObservableObject
{
    // Servicio que maneja la lógica de obtención de artículos
    private readonly ServicioCompras _servicio;

    // Colección observable que contiene los artículos (se refleja automáticamente en la UI)
    [ObservableProperty] private ObservableCollection<Articulo> articulos = new();

    // Campos de entrada del formulario (enlazados a la UI)
    [ObservableProperty] private string nuevoNombre = string.Empty;       // Nombre del nuevo producto
    [ObservableProperty] private string nuevaCantidadTexto = string.Empty; // Cantidad como texto (para validar)
    [ObservableProperty] private string nuevasNotas = string.Empty;        // Notas adicionales

    // Mensaje de error mostrado si hay validaciones fallidas
    [ObservableProperty] private string mensajeError = string.Empty;

    // Constructor: inicializa el servicio y carga algunos artículos de ejemplo
    public ListaComprasViewModel()
    {
        _servicio = new ServicioCompras();
        Articulos = new ObservableCollection<Articulo>(_servicio.ObtenerEjemplo());
    }

    // Comando para agregar un nuevo artículo a la lista
    [RelayCommand]
    private void AgregarArticulo()
    {
        // Limpiar mensaje de error previo
        MensajeError = string.Empty;

        // Validar que se haya escrito un nombre
        if (string.IsNullOrWhiteSpace(NuevoNombre))
        {
            MensajeError = "Escribe el nombre del producto.";
            return;
        }

        // Validar cantidad (por defecto 1 si no se especifica)
        int cantidad = 1;
        if (!string.IsNullOrWhiteSpace(NuevaCantidadTexto))
        {
            // Verificar que la cantidad sea un número entero positivo
            if (!int.TryParse(NuevaCantidadTexto, out cantidad) || cantidad <= 0)
            {
                MensajeError = "Cantidad inválida. Usa un número mayor que 0.";
                return;
            }
        }

        // Agregar el nuevo artículo a la lista
        Articulos.Add(new Articulo
        {
            Nombre = NuevoNombre.Trim(),
            Cantidad = cantidad,
            Notas = NuevasNotas?.Trim() ?? string.Empty,
            Comprado = false
        });

        // Limpiar los campos del formulario después de agregar
        NuevoNombre = string.Empty;
        NuevaCantidadTexto = string.Empty;
        NuevasNotas = string.Empty;
    }

    // Comando para eliminar un artículo seleccionado
    [RelayCommand]
    private void EliminarArticulo(Articulo? articulo)
    {
        if (articulo is null) return; // Si es nulo, no hacer nada
        if (Articulos.Contains(articulo))
            Articulos.Remove(articulo); // Quitarlo de la lista
    }

    // Comando para vaciar completamente la lista
    [RelayCommand]
    private void VaciarLista()
    {
        Articulos.Clear(); // Elimina todos los elementos
    }
}
