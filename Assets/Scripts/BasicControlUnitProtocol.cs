using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

namespace Landau
{
    public class BasicControlUnitProtocol : IControlUnitProtocol
    {
        public ProtocolState State { get; private set; }
        public ControlUnit ControlUnit { get; set; }

        private WebSocket _webSocket = null;
        private string _webSocketUrl = "ws://localhost:5880";

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
            State = ProtocolState.DisconnectedState;

            _webSocket = new WebSocket(_webSocketUrl);
            _webSocket.OnMessage += (sender, e) =>
                Decode(e.Data);

            _webSocket.OnClose += ConnectionClosedThreaded;
            _webSocket.OnError += ConnectionClosedThreaded;
            _webSocket.OnOpen += ConnectionOpenedThreaded;

            ConnectToWebSocket();
        }

        private void ConnectToWebSocket()
        {
            Debug.Log("Connecting...");
            _webSocket.ConnectAsync();
        }

        private IEnumerator ConnectToWebSocketDelayed()
        {
            yield return new WaitForSeconds(1);
            ConnectToWebSocket();
        }

        private void ConnectionClosedThreaded(object sender, EventArgs e)
        {
            UnityMainThreadDispatcher.Instance().Enqueue(ConnectionClosed());
        }

        private void ConnectionOpenedThreaded(object sender, EventArgs e)
        {
            UnityMainThreadDispatcher.Instance().Enqueue(ConnectionOpened());
        }

        private IEnumerator ConnectionClosed()
        {
            if (State != ProtocolState.DisconnectedState)
            {
                State = ProtocolState.DisconnectedState;
                Disconnected(this, null);
            }

            Debug.Log("Disconnect");
            yield return ConnectToWebSocketDelayed();
        }

        private IEnumerator ConnectionOpened()
        {
            if (State != ProtocolState.ConnectedState)
            {
                State = ProtocolState.ConnectedState;
                Connected(this, null);
            }
            yield return null;
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
