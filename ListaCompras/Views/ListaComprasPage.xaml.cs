using CommunityToolkit.Mvvm.Messaging;
using ListaCompras.Messages;
using ListaCompras.ViewModels;

namespace ListaCompras.Views
{
    public partial class ListaComprasPage : ContentPage
    {
        private readonly ListaComprasViewModel _vm;

        public ListaComprasPage(ListaComprasViewModel vm)
        {
            InitializeComponent();
            _vm = vm;
            BindingContext = vm;

            // Nos registramos para escuchar los mensajes de notificación
            WeakReferenceMessenger.Default.Register<UiNotificationMessage>(this, async (r, m) =>
            {
                // Aquí mostramos el mensaje (puedes cambiar DisplayAlert por snackbar si usas CommunityToolkit.Maui)
                await DisplayAlert("Cambios guardados", m.Value, "OK");
            });
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _vm.CargarAsync();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            // Para evitar fugas de memoria
            WeakReferenceMessenger.Default.Unregister<UiNotificationMessage>(this);
        }
    }
}
