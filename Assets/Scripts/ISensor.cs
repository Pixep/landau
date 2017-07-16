namespace Landau
{
    public interface ISensor
    {
        ISensorObserver Observer { get; set; }
        SensorPayload GetValue();
    }
}