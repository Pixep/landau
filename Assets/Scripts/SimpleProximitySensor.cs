using System;
using UnityEngine;

namespace Landau
{
    public class SimpleProximitySensor : MonoBehaviour, IProximitySensor
    {
        public void Start()
        {
            SensorsManager.Instance().RegisterSensor(this);
        }

        public bool IsTriggered { get; private set; }
        public SensorPayload GetValue()
        {
            byte[] data = new byte[1];
            data[0] = Convert.ToByte(IsTriggered);
            return new SensorPayload(0, 0, data);
        }

        private void OnTriggerEnter(Collider other)
        {
            IsTriggered = true;
        }
        private void OnTriggerExit(Collider other)
        {
            IsTriggered = false;
        }
    }
}