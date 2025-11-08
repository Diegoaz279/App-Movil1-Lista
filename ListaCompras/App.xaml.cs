using ListaCompras.Views;

namespace ListaCompras
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            // Recomendado en .NET 9
            return new Window(new ListaComprasPage());
        }
    }
}