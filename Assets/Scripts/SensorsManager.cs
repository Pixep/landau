using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Landau
{
    public class SensorsManager : ISensorObserver
    {
        private ISensorsProtocol m_protocol;
        private List<ISensor> m_sensors = new List<ISensor>();

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
            m_protocol = Main.Factory.CreateSensorsProtocol(this);
        }

        // Register a sensor
        public void RegisterSensor(ISensor sensor)
        {
            m_sensors.Add(sensor);
            sensor.Observer = this;
        }

        // Update is called once per frame
        void Update()
        {
        }

        public void Notify(ISensor sensor)
        {
            SensorPayload payload = sensor.GetValue();
            m_protocol.SendValue(payload);
        }
    }
}