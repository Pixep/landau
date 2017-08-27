using System;

namespace Landau
{
    public enum ProtocolState
    {
        DisconnectedState,
        ConnectingState,
        ConnectedState
    }

    public interface IProtocol
    {
        ProtocolState State { get; }

        event EventHandler Disconnected;
        event EventHandler Connecting;
        event EventHandler Connected;
    }
}
