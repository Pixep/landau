using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Landau
{
    public class SensorsManager : MonoBehaviour
    {
        private MainFactory m_factory = new MainFactory();
        private ISensorsProtocol m_protocol;

        // Use this for initialization
        void Start()
        {
            m_protocol = m_factory.CreateSensorsProtocol(this);
        }

        // Update is called once per frame
        private int it = 0;
        void Update()
        {
            ++it;
            if (it >= 100)
            {
                string value = "854Dodo";
                SensorPayload payload = new SensorPayload(1, 35, Encoding.ASCII.GetBytes(value));
                m_protocol.SendValue(payload);
                it = 0;
            }
        }
    }
}