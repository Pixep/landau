using System;
using UnityEngine;
using WebSocketSharp;

namespace Landau
{
    public class BasicControlUnitProtocol : IControlUnitProtocol
    {
        public MainControlUnit ControlUnit { get; set; }

        private WebSocket m_webSocket = null;
        private string m_webSocketUrl = "ws://localhost:5880";

        [Serializable]
        private class Command
        {
            public CommandType id = 0;
            public enum CommandType
            {
                ControlType
            }
        }

        [Serializable]
        private class ControlValues
        {
            public float steering = 0;
            public float acceleration = 0;
            public float brake = 0;
            public float handBrake = 0;
            static public ControlValues FromJson(string json)
            {
                return JsonUtility.FromJson<ControlValues>(json);
            }
        }

        public BasicControlUnitProtocol(MainControlUnit controlUnit)
        {
            ControlUnit = controlUnit;
            ConnectToWebSocket();
        }

        private void ConnectToWebSocket()
        {
            m_webSocket = new WebSocket(m_webSocketUrl);
            m_webSocket.OnMessage += (sender, e) =>
                Decode(e.Data);

            m_webSocket.Connect();
        }

        /*
         * Decodes a json data payload to operate the vehicle
         */
        public void Decode(string json)
        {
            if (!ControlUnit)
            {
                Debug.Log("No control unit set in procotol, message dropped");
                return;
            }

            Command command = JsonUtility.FromJson<Command>(json);
            switch (command.id)
            {
                case Command.CommandType.ControlType:
                    ControlValues controlValues = ControlValues.FromJson(json);
                    ControlUnit.Steering = controlValues.steering;
                    ControlUnit.Acceleration = controlValues.acceleration;
                    ControlUnit.Brake = controlValues.brake;
                    ControlUnit.HandBrake = controlValues.handBrake;
                    break;
                default:
                    break;
            }
        }
    }
}
