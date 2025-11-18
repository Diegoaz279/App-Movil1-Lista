using System.Threading.Tasks;
using Microsoft.Maui.Media;

namespace ListaCompras.Services
{
    public interface IFotoService
    {
        Task<string?> SeleccionarFotoAsync();
    }

    public class FotoService : IFotoService
    {
        public async Task<string?> SeleccionarFotoAsync()
        {
            try
            {
                var result = await MediaPicker.Default.PickPhotoAsync();
                return result?.FullPath;
            }
            catch
            {
                return null;
            }
        }
    }
}
