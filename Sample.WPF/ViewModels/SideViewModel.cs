using CommunityToolkit.Mvvm.ComponentModel;
using Sample.WPF.Messages;
using System.Drawing;
using System.Windows.Media;


namespace Sample.WPF.ViewModels;

[RegisterService]
public partial class SideViewModel: ObservableObject
{
    [ObservableProperty] SolidColorBrush _connectionColor = new(Colors.Transparent);

    public SideViewModel()
    {
        ConnectionMessage.Register(this, OnConnectioMessageReceived);
    }

    private void OnConnectioMessageReceived(object recipient, ConnectionMessage message)
    {
        this.ConnectionColor = new(message.Value ? Colors.Green : Colors.Red);
    }
}
