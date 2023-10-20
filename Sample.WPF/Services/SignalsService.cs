namespace Sample.WPF.Services;

[RegisterService(ImplementationType = typeof(SignalsService), InstanceType = InstanceType.Singleton)]
public interface ISignalsService
{
    bool ToogleConnection();
}

public class SignalsService : ISignalsService
{
    private bool connected = false;

    public bool ToogleConnection()
    {
        this.connected = !connected;
        return connected;
    }
}
