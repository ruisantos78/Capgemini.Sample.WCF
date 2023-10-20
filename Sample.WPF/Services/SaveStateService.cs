using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sample.WPF.Services;

[RegisterService(ImplementationType = typeof(SaveStateService), InstanceType = InstanceType.Singleton)]
public interface ISaveStateService
{
    Task StoreAsync<TState>(ObservableCollection<TState> values);
    Task<ObservableCollection<TState>> LoadAsync<TState>();
}

internal class SaveStateService : ISaveStateService, IDisposable
{
    private readonly DirectoryInfo source = new(Path.Combine(Environment.CurrentDirectory, "States"));
    private readonly Dictionary<string, object> cache = new();
    private readonly BackgroundWorker worker = new();

    public SaveStateService()
    {
        worker.DoWork += OnSaveStates;
        worker.RunWorkerCompleted += OnWorkerCompleted;

        InitializeService();
    }

    private void OnWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
    {
        Task.Delay(500).ContinueWith(_ => worker.RunWorkerAsync());
    }

    public void Dispose()
    {
        worker.CancelAsync();
    }

    private void InitializeService()
    {
        if (!source.Exists)
            source.Create();

        worker.RunWorkerAsync();
    }

    private void OnSaveStates(object? sender, DoWorkEventArgs e)
    {
        foreach (var typeName in cache.Keys)
        {
            if (worker.CancellationPending)
                return;

            lock (cache[typeName])
            {
                if (cache[typeName] is null)
                    continue;
                
                using (var file = GetFile(typeName).Open(FileMode.Create, FileAccess.Write))
                    JsonSerializer.Serialize(file, cache[typeName]);
            }
        }
    }

    private FileInfo GetFile(string typeName)
    {
        var filename = Path.ChangeExtension(typeName, ".json");
        return new FileInfo(Path.Combine(source.FullName, filename!));
    }

    public Task StoreAsync<TState>(ObservableCollection<TState> values)
    {
        cache[typeof(TState).FullName!] = values;
        return Task.CompletedTask;
    }

    public async Task<ObservableCollection<TState>> LoadAsync<TState>()
    {
        var file = GetFile(typeof(TState).FullName!);
        if (!file.Exists)
            return new();

        using var stream = file.OpenRead();
        return await JsonSerializer.DeserializeAsync<ObservableCollection<TState>>(stream) ?? new();        
    }
}
