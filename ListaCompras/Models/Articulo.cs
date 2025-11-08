using CommunityToolkit.Mvvm.ComponentModel;

namespace ListaCompras.Models;

public partial class Articulo : ObservableObject
{
    [ObservableProperty]
    private string nombre = string.Empty;

    [ObservableProperty]
    private int cantidad;

    [ObservableProperty]
    private bool comprado;

    [ObservableProperty]
    private string notas = string.Empty;
}
