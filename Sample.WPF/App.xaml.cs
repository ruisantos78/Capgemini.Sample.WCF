using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace Sample.WPF;

public partial class App : Application
{
    public new static App Current => (App)Application.Current;

    public IServiceProvider Services { get; }

    public App()
    {
        this.Services = ConfigureServices();   
    }

    private static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();
        
        services.RegisterServices();
        
        return services.BuildServiceProvider();
    }
}
