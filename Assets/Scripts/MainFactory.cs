namespace Landau
{
    public class MainFactory
    {
        public IControlUnitProtocol CreateControlProtocol(ControlUnit controlUnit)
        {
            return new BasicControlUnitProtocol(controlUnit);
        }

        public ISensorsProtocol CreateSensorsProtocol(SensorsManager sensorsManager)
        {
            return new BasicSensorsProtocol(sensorsManager);
        }
    }
}