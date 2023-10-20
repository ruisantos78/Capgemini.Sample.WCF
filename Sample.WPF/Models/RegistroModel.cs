using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace Sample.WPF.Models;

public partial class RegistroModel: ObservableObject
{
    [ObservableProperty] Guid _id = Guid.NewGuid();
    [ObservableProperty] string _name = string.Empty;
    [ObservableProperty] int _value = 0;
}
