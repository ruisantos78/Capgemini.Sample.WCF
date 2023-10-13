using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Sample.WPF.Messages;
using Sample.WPF.Services;

namespace Sample.WPF.ViewModels;

[RegisterService]
[ObservableObject]
public partial class HomeViewModel: ViewModelBase
{
    private readonly ISignalsService signalsService;

    public HomeViewModel(ISignalsService signalsService)
    {
        this.signalsService = signalsService;
    }

    [RelayCommand]
    public void ToggleConnection()
    {
        this.signalsService.ToogleConnection();        
    }
}
