using System;

namespace Landau
{
    public interface IControlUnitProtocol: IProtocol
    {
        ControlUnit ControlUnit { get; set; }
        void Decode(string data);
    }
}
