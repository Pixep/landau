using System;

namespace Landau
{
    public enum ProtocolState {
        DisconnectedState,
        ConnectingState,
        ConnectedState
    }

    public interface IControlUnitProtocol
    {
        ProtocolState State { get; }
        ControlUnit ControlUnit { get; set; }
        void Decode(string data);

        event EventHandler Disconnected;
        event EventHandler Connecting;
        event EventHandler Connected;
    }
}
