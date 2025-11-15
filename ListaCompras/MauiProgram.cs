using Microsoft.Extensions.Logging;

namespace ListaCompras
{
    // Clase estática que configura y construye la aplicación MAUI
    public static class MauiProgram
    {
        // Método principal que crea y devuelve la instancia de la app MAUI
        public static MauiApp CreateMauiApp()
        {
            // Crea un nuevo "builder" que define los servicios y configuraciones de la app
            var builder = MauiApp.CreateBuilder();

            builder
                // Registra la clase App.xaml.cs como la aplicación principal
                .UseMauiApp<App>()

                // Configura las fuentes personalizadas que se usarán en toda la app
                .ConfigureFonts(fonts =>
                {
                    // Fuente regular: se puede usar como "OpenSansRegular" en XAML o C#
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");

                    // Fuente semibold (más gruesa): alias "OpenSansSemibold"
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            // Solo en modo depuración: habilita el registro (logging) para ver mensajes en la consola
            builder.Logging.AddDebug();
#endif

            // Finalmente construye la aplicación y la devuelve
            return builder.Build();
        }
    }
}
