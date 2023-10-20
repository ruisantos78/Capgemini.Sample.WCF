using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Sample.WPF.Messages;
using Sample.WPF.Models;
using Sample.WPF.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.WPF.ViewModels;

[RegisterService]
public sealed partial class HomeViewModel : ObservableObject
{
    private readonly ISaveStateService saveStateService;
    private readonly ISignalsService signalsService;

    [ObservableProperty] private ObservableCollection<RegistroModel> _registros = new();

    public HomeViewModel(
        ISaveStateService saveStateService,
        ISignalsService signalsService)
    {
        this.saveStateService = saveStateService;
        this.signalsService = signalsService;
    }

    [RelayCommand]
    public async Task InitializeAsync()
    {
        
        this.Registros = await saveStateService.LoadAsync<RegistroModel>();        
    }

    [RelayCommand]
    public void ToggleConnection()
    {
        var state = this.signalsService.ToogleConnection();
        ConnectionMessage.Send(state);
    }
}
