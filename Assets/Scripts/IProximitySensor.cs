using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Landau
{
    public interface IBinarySensor : ISensor
    {
        bool Value { get; }
    }
}
