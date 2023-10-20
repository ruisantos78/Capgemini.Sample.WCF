using Sample.WPF.ViewModels;
using System.Windows.Controls;

namespace Sample.WPF.Views;

/// <summary>
/// Interação lógica para HomeView.xam
/// </summary>
public partial class HomeView : Page
{
    public HomeView()
    {
        DataContext = App.Current.Services.GetService(typeof(HomeViewModel));

        InitializeComponent();
    }
}
