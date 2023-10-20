using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Sample.WPF.Messages;
using System;
using System.Windows.Media;


namespace Sample.WPF.ViewModels;

[RegisterService]
public sealed partial class SideViewModel: ObservableObject, IDisposable
{
    [ObservableProperty] SolidColorBrush _connectionColor = new(Colors.Transparent);

    public SideViewModel()
    {
        ConnectionMessage.Register(this, OnConnectioMessageReceived);
    }

    public void Dispose()
    {
        WeakReferenceMessenger.Default.UnregisterAll(this);        
    }

    private void OnConnectioMessageReceived(object recipient, ConnectionMessage message)
    {
        this.ConnectionColor = new(message.Value ? Colors.Green : Colors.Red);
    }

    
}
