using System.IO;
using ListaCompras.Services;
using ListaCompras.ViewModels;
using ListaCompras.Views;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Storage;

namespace ListaCompras
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            // BD SQLite
            builder.Services.AddSingleton<AppDatabase>(svc =>
            {
                var path = Path.Combine(FileSystem.AppDataDirectory, "compras.db3");
                return new AppDatabase(path);
            });

            // Servicios
            builder.Services.AddSingleton<ServicioCompras>();
            builder.Services.AddSingleton<IFotoService, FotoService>();

            // ViewModel y Página
            builder.Services.AddSingleton<ListaComprasViewModel>();
            builder.Services.AddSingleton<ListaComprasPage>();

            return builder.Build();
        }
    }
}
