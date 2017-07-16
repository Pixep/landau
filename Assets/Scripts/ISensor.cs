namespace Landau
{
    public interface ISensor
    {
        byte Id { get; set; }
        byte Type { get; set; }
        ISensorObserver Observer { get; set; }
        SensorPayload GetValue();
    }
}