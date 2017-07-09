using System;

namespace Landau
{
    [Serializable]
    public class SensorPayload
    {
        public SensorPayload(byte _id, byte _type, byte[] _value)
        {
            id = _id;
            type = _type;
            value = _value;
        }
        public byte id;
        public byte type;
        public byte[] value;
    }

    public interface ISensorsProtocol
    {
        void SendValue(SensorPayload payload);
    }
}
