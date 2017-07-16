namespace Landau
{
    public class MainFactory
    {
        public IControlUnitProtocol CreateControlProtocol(MainControlUnit controlUnit)
        {
            return new BasicControlUnitProtocol(controlUnit);
        }

        public ISensorsProtocol CreateSensorsProtocol(SensorsManager sensorsManager)
        {
            return new BasicSensorsProtocol(sensorsManager);
        }
    }
}