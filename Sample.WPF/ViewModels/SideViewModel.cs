using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Sample.WPF.Messages;
using System;
using System.Windows.Media;


namespace Sample.WPF.ViewModels;

[RegisterService]
[ObservableObject]
public partial class SideViewModel: ViewModelBase, IDisposable
{
    [ObservableProperty] SolidColorBrush _connectionColor = new(Colors.Transparent);
    [ObservableProperty] string _connectionStatus = string.Empty;

    public SideViewModel()
    {
        ConnectionMessage.Register(this, OnConnectioMessageReceived);
    }

    public void Dispose()
    {        
        WeakReferenceMessenger.Default.UnregisterAll(this);
    }

    partial void OnConnectionStatusChanged(string value) 
    {
        this.ConnectionColor = value switch {
            "On-line" => new SolidColorBrush(Colors.Green),
            "Off-line" => new SolidColorBrush(Colors.Red),
            _ => new SolidColorBrush(Colors.Transparent),
        };
    }

    private void OnConnectioMessageReceived(object recipient, ConnectionMessage message)
    {
        this.ConnectionStatus = message.Value ? "On-line" : "Off-line";        
    }
}
