using System.Collections.Generic;

namespace Landau
{
    public class SensorsManager : ISensorObserver
    {
        private ISensorsProtocol _protocol;
        private List<ISensor> _sensors = new List<ISensor>();

        public ISensorsProtocol Protocol { get { return _protocol; } }

        static private SensorsManager m_instance;
        static public SensorsManager Instance()
        {
            if (m_instance == null)
                m_instance = new SensorsManager();

            return m_instance;
        }

        SensorsManager()
        {
            m_instance = this;
            _protocol = Main.Factory.CreateSensorsProtocol(this);
        }

        // Register a sensor
        public void RegisterSensor(ISensor sensor)
        {
            _sensors.Add(sensor);
            sensor.Observer = this;
        }

        // Update is called once per frame
        void Update()
        {
        }

        public void Notify(ISensor sensor)
        {
            SensorPayload payload = sensor.GetValue();
            _protocol.SendValue(payload);
        }
    }
}