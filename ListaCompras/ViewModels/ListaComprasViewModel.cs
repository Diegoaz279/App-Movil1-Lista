using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ListaCompras.Models;
using ListaCompras.Services;
using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Messaging;
using ListaCompras.Messages;
using System.Linq;
using System.Threading.Tasks;

namespace ListaCompras.ViewModels
{
    public partial class ListaComprasViewModel : ObservableObject
    {
        private readonly ServicioCompras _servicio;
        private readonly IFotoService _fotoService;

        // Lista completa (para filtros)
        private readonly ObservableCollection<Articulo> _todos = new();

        // Lista que se muestra en la UI
        [ObservableProperty]
        private ObservableCollection<Articulo> articulos = new();

        // Campos del formulario
        [ObservableProperty] private string nuevoNombre = string.Empty;
        [ObservableProperty] private string nuevaCantidadTexto = string.Empty;
        [ObservableProperty] private string nuevasNotas = string.Empty;

        [ObservableProperty] private string mensajeError = string.Empty;

        // Filtros
        [ObservableProperty] private string textoBusqueda = string.Empty;
        [ObservableProperty] private string categoriaSeleccionada = "Todas";

        public ObservableCollection<string> Categorias { get; } =
            new ObservableCollection<string>
            {
                "Todas",
                "Verduras",
                "Carnes",
                "Lácteos",
                "Bebidas",
                "Limpieza",
                "Otros"
            };

        // Foto seleccionada para el artículo actual
        [ObservableProperty] private string? imagenSeleccionadaPath;

        // Modo edición
        [ObservableProperty] private bool estaEditando;
        [ObservableProperty] private Articulo? articuloEnEdicion;

        public ListaComprasViewModel(ServicioCompras servicio, IFotoService fotoService)
        {
            _servicio = servicio;
            _fotoService = fotoService;
        }

        // Se llamará al iniciar la página
        [RelayCommand]
        public async Task CargarAsync()
        {
            MensajeError = string.Empty;

            _todos.Clear();
            Articulos.Clear();

            var lista = await _servicio.ObtenerTodosAsync();

            foreach (var a in lista.OrderBy(x => x.Nombre))
            {
                _todos.Add(a);
                Articulos.Add(a);
            }
        }

        // Agregar o guardar cambios
        [RelayCommand]
        private async Task GuardarArticuloAsync()
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

            var categoria = CategoriaSeleccionada;
            if (string.IsNullOrWhiteSpace(categoria) || categoria == "Todas")
                categoria = "Otros";

            if (EstaEditando && ArticuloEnEdicion is not null)
            {
                // Editar
                ArticuloEnEdicion.Nombre = NuevoNombre.Trim();
                ArticuloEnEdicion.Cantidad = cantidad;
                ArticuloEnEdicion.Notas = NuevasNotas?.Trim() ?? string.Empty;
                ArticuloEnEdicion.Categoria = categoria;
                ArticuloEnEdicion.ImagenPath = ImagenSeleccionadaPath;

                await _servicio.GuardarAsync(ArticuloEnEdicion);
            }
            else
            {
                // Nuevo
                var nuevo = new Articulo
                {
                    Nombre = NuevoNombre.Trim(),
                    Cantidad = cantidad,
                    Notas = NuevasNotas?.Trim() ?? string.Empty,
                    Comprado = false,
                    Categoria = categoria,
                    ImagenPath = ImagenSeleccionadaPath
                };

                await _servicio.GuardarAsync(nuevo);
                _todos.Add(nuevo);
            }
            WeakReferenceMessenger.Default.Send(new UiNotificationMessage("Artículo guardado correctamente."));


            LimpiarFormulario();
            AplicarFiltros();
        }

        // Se ejecuta al tocar un ítem de la lista
        [RelayCommand]
        private void EditarArticulo(Articulo articulo)
        {
            if (articulo is null) return;

            EstaEditando = true;
            ArticuloEnEdicion = articulo;

            NuevoNombre = articulo.Nombre;
            NuevaCantidadTexto = articulo.Cantidad.ToString();
            NuevasNotas = articulo.Notas;
            ImagenSeleccionadaPath = articulo.ImagenPath;
            CategoriaSeleccionada = string.IsNullOrEmpty(articulo.Categoria)
                ? "Otros"
                : Categorias.Contains(articulo.Categoria) ? articulo.Categoria : "Otros";
        }

        [RelayCommand]
        private async Task EliminarArticuloAsync(Articulo articulo)
        {
            if (articulo is null) return;

            await _servicio.EliminarAsync(articulo);
            Articulos.Remove(articulo);

            WeakReferenceMessenger.Default.Send(new UiNotificationMessage("Artículo eliminado de la lista."));
        }


        [RelayCommand]
        private async Task VaciarListaAsync()
        {
            await _servicio.VaciarAsync();
            Articulos.Clear();

            WeakReferenceMessenger.Default.Send(new UiNotificationMessage("Lista vaciada correctamente."));
        }


        // Seleccionar foto
        [RelayCommand]
        private async Task SeleccionarFotoAsync()
        {
            var path = await _fotoService.SeleccionarFotoAsync();
            if (!string.IsNullOrEmpty(path))
                ImagenSeleccionadaPath = path;
        }

        [RelayCommand]
        private void CancelarEdicion()
        {
            LimpiarFormulario();
        }

        partial void OnTextoBusquedaChanged(string value) => AplicarFiltros();
        partial void OnCategoriaSeleccionadaChanged(string value) => AplicarFiltros();

        private void AplicarFiltros()
        {
            var query = _todos.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(TextoBusqueda))
            {
                var q = TextoBusqueda.Trim().ToLower();
                query = query.Where(a =>
                    (a.Nombre?.ToLower().Contains(q) ?? false) ||
                    (a.Notas?.ToLower().Contains(q) ?? false));
            }

            if (!string.IsNullOrWhiteSpace(CategoriaSeleccionada) &&
                CategoriaSeleccionada != "Todas")
            {
                query = query.Where(a => a.Categoria == CategoriaSeleccionada);
            }

            var resultado = query.OrderBy(a => a.Nombre).ToList();

            Articulos.Clear();
            foreach (var a in resultado)
                Articulos.Add(a);
        }

        private void LimpiarFormulario()
        {
            NuevoNombre = string.Empty;
            NuevaCantidadTexto = string.Empty;
            NuevasNotas = string.Empty;
            ImagenSeleccionadaPath = null;
            ArticuloEnEdicion = null;
            EstaEditando = false;
            MensajeError = string.Empty;
        }
    }
}
