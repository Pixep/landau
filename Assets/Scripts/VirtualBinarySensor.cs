using System;
using UnityEngine;

namespace Landau
{
    public class VirtualBinarySensor : MonoBehaviour, IBinarySensor
    {
        [SerializeField] public byte _Id;
        [SerializeField] public byte _Type;

        public byte Id { get { return _Id; } set { _Id = value; } }
        public byte Type { get { return _Type; } set { _Type = value; } }

        public bool Value { get; protected set; }
        public ISensorObserver Observer { get; set; }

        public void Start()
        {
            SensorsManager.Instance().RegisterSensor(this);
        }
        
        public SensorPayload GetValue()
        {
            byte[] data = new byte[1];
            data[0] = Convert.ToByte(Value);
            return new SensorPayload(_Id, _Type, data);
        }

        protected void NotifyObserver()
        {
            if (Observer != null)
                Observer.Notify(this);
        }
    }
}