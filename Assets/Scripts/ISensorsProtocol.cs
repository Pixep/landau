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

        public SensorPayload(byte _id, byte _type, bool _value)
        {
            id = _id;
            type = _type;
            value = new byte[1];
            value[0] = Convert.ToByte(_value);
        }

        public byte id;
        public byte type;
        public byte[] value;
        private int v1;
        private int v2;
        private bool v3;
    }

    public interface ISensorsProtocol: IProtocol
    {
        void SendValue(SensorPayload payload);
    }
}
