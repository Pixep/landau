namespace Landau
{
    public interface IControlUnitProtocol
    {
        MainControlUnit ControlUnit { get; set; }
        void Decode(string data);
    }
}
