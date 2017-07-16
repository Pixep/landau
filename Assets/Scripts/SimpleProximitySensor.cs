using System;
using UnityEngine;

namespace Landau
{
    public class SimpleProximitySensor : MonoBehaviour, IProximitySensor
    {
        public bool IsTriggered { get; private set; }
        public ISensorObserver Observer { get; set; }

        public void Start()
        {
            SensorsManager.Instance().RegisterSensor(this);
        }
        
        public SensorPayload GetValue()
        {
            byte[] data = new byte[1];
            data[0] = Convert.ToByte(IsTriggered);
            return new SensorPayload(0, 0, data);
        }

        private void OnTriggerEnter(Collider other)
        {
            IsTriggered = true;
            NotifyObserver();
        }

        private void OnTriggerExit(Collider other)
        {
            IsTriggered = false;
            NotifyObserver();
        }

        private void NotifyObserver()
        {
            if (Observer != null)
                Observer.Notify(this);
        }
    }
}