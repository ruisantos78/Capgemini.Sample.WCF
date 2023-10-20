using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Sample.WPF.Messages;

public class ConnectionMessage : ValueChangedMessage<bool>
{
    public ConnectionMessage(bool value) : base(value)
    {
    }

    public static void Send(bool connected)
        => WeakReferenceMessenger.Default.Send(new ConnectionMessage(connected));

    public static void Register(object recipent, MessageHandler<object, ConnectionMessage> messageHandler)
        => WeakReferenceMessenger.Default.Register(recipent, messageHandler);
}
