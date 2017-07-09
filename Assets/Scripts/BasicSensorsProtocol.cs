﻿using System;
using System.Text;
using UnityEngine;
using WebSocketSharp;

namespace Landau {
    public class BasicSensorsProtocol : ISensorsProtocol
    {
        public SensorsManager Sensors { get; set; }

        private WebSocket m_webSocket = null;
        private string m_webSocketUrl = "ws://localhost:5881";

        public BasicSensorsProtocol(SensorsManager sensors)
        {
            Sensors = sensors;
            ConnectToWebSocket();
        }

        /*
         * Publish values
         */
        public void SendValue(SensorPayload payload)
        {
            // Ignore sensor if no connection
            if (!m_webSocket.IsAlive)
                return;

            string payloadString = JsonUtility.ToJson(payload);
            m_webSocket.SendAsync(Encoding.ASCII.GetBytes(payloadString), null);
        }

        private void ConnectToWebSocket()
        {
            m_webSocket = new WebSocket(m_webSocketUrl);
            m_webSocket.ConnectAsync();
        }
    }
}