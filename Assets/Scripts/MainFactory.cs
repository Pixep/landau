namespace Landau
{
    public class MainFactory
    {
        public IControlUnitProtocol CreateProtocol(MainControlUnit controlUnit)
        {
            return new BasicControlUnitProtocol(controlUnit);
        }
    }
}