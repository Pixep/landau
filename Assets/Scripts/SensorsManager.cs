using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Landau
{
    public class SensorsManager : MonoBehaviour, ISensorObserver
    {
        private MainFactory m_factory = new MainFactory();
        private ISensorsProtocol m_protocol;
        private List<ISensor> m_sensors = new List<ISensor>();

        static private SensorsManager m_instance;
        static public SensorsManager Instance()
        {
            return m_instance;
        }

        SensorsManager()
        {
            m_instance = this;
        }

        // Use this for initialization
        void Start()
        {
            m_protocol = m_factory.CreateSensorsProtocol(this);
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