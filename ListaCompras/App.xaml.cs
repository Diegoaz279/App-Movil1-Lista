namespace ListaCompras
{
    public partial class App : Application
    {
        // La página principal se resuelve por inyección de dependencias (DI)
        public App(Views.ListaComprasPage mainPage)
        {
            InitializeComponent();

            // Si quieres barra de navegación, usa NavigationPage
            MainPage = new NavigationPage(mainPage);

            // Si NO quieres barra de navegación:
            // MainPage = mainPage;
        }
    }
}
