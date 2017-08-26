using System;
using UnityEngine;
using WebSocketSharp;

namespace Landau
{
    public class BasicControlUnitProtocol : IControlUnitProtocol
    {
        public ProtocolState State { get; private set; }
        public ControlUnit ControlUnit { get; set; }

        private WebSocket m_webSocket = null;
        private string m_webSocketUrl = "ws://localhost:5880";

        public event EventHandler Disconnected;
        public event EventHandler Connecting;
        public event EventHandler Connected;

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

        public BasicControlUnitProtocol(ControlUnit controlUnit)
        {
            ControlUnit = controlUnit;
            ConnectToWebSocket();
        }

        private void ConnectToWebSocket()
        {
            m_webSocket = new WebSocket(m_webSocketUrl);
            m_webSocket.OnMessage += (sender, e) =>
                Decode(e.Data);

            m_webSocket.OnClose += ConnectionClosed;
            m_webSocket.OnError += ConnectionClosed;
            m_webSocket.OnOpen += ConnectionOpened;

            m_webSocket.ConnectAsync();
        }

        private void ConnectionClosed(object sender, EventArgs e)
        {
            State = ProtocolState.DisconnectedState;
            Disconnected(this, null);
        }

        private void ConnectionOpened(object sender, EventArgs e)
        {
            State = ProtocolState.ConnectedState;
            Connected(this, null);
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
