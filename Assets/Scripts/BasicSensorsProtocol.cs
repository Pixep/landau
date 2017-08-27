using System;
using System.Collections;
using UnityEngine;
using WebSocketSharp;

namespace Landau
{
    public class BasicSensorsProtocol : ISensorsProtocol
    {
        public ProtocolState State { get; private set; }
        public event EventHandler Disconnected;
        public event EventHandler Connecting;
        public event EventHandler Connected;

        public SensorsManager Sensors { get; set; }

        private WebSocket _webSocket = null;
        private string _webSocketUrl = "ws://localhost:5881";
 
        public BasicSensorsProtocol(SensorsManager sensors)
        {
            Sensors = sensors;
            State = ProtocolState.DisconnectedState;

            _webSocket = new WebSocket(_webSocketUrl);

            _webSocket.OnClose += ConnectionClosedThreaded;
            _webSocket.OnError += ConnectionClosedThreaded;
            _webSocket.OnOpen += ConnectionOpenedThreaded;

            ConnectToWebSocket();
        }

        private void ConnectToWebSocket()
        {
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
                if (Disconnected != null)
                    Disconnected(this, null);
            }

            yield return ConnectToWebSocketDelayed();
        }

        private IEnumerator ConnectionOpened()
        {
            if (State != ProtocolState.ConnectedState)
            {
                State = ProtocolState.ConnectedState;
                if (Connected != null)
                    Connected(this, null);
            }
            yield return null;
        }

        /*
         * Publish values
         */
        public void SendValue(SensorPayload payload)
        {
            // Ignore sensor if no connection
            if (!_webSocket.IsAlive)
                return;

            string payloadString = JsonUtility.ToJson(payload);
            _webSocket.Send(payloadString);
        }
    }
}