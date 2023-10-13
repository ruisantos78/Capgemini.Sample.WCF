using Sample.WPF.Messages;

namespace Sample.WPF.Services;

[RegisterService(ImplementationType = typeof(SignalsService), InstanceType = InstanceType.Singleton)]
public interface ISignalsService
{
    void ToogleConnection();
}

public class SignalsService : ISignalsService
{
    private bool connected = false;

    public void ToogleConnection()
    {
        this.connected = !connected;
        ConnectionMessage.Send(connected);;
    }
}
