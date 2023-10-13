using Sample.WPF.ViewModels;
using System.Windows.Controls;

namespace Sample.WPF.Views;

/// <summary>
/// Interação lógica para SideView.xam
/// </summary>
public partial class SideView : Page
{
    public SideView()
    {
        DataContext = App.Current.Services.GetService(typeof(SideViewModel));
        InitializeComponent();
    }
}
