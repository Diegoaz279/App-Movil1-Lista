using CommunityToolkit.Mvvm.Messaging.Messages;

namespace ListaCompras.Messages
{
    // Mensaje simple que solo lleva un string con el texto a mostrar
    public class UiNotificationMessage : ValueChangedMessage<string>
    {
        public UiNotificationMessage(string value) : base(value)
        {
        }
    }
}
