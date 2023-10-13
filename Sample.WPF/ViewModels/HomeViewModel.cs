using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Sample.WPF.Messages;
using Sample.WPF.Services;

namespace Sample.WPF.ViewModels;

[RegisterService]
public partial class HomeViewModel: ObservableObject
{
    private readonly ISignalsService signalsService;

    public HomeViewModel(ISignalsService signalsService)
    {
        this.signalsService = signalsService;
    }

    [RelayCommand]
    public void ToggleConnection()
    {
        var state = this.signalsService.ToogleConnection();
        ConnectionMessage.Send(state);
    }
}
