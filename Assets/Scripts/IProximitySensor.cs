using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Landau
{
    public interface IProximitySensor : ISensor
    {
        bool IsTriggered { get; }
    }
}
