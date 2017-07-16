using System;
using UnityEngine;

namespace Landau
{
    public class SimpleProximitySensor : VirtualBinarySensor
    {
        private void OnTriggerEnter(Collider other)
        {
            Value = true;
            NotifyObserver();
        }

        private void OnTriggerExit(Collider other)
        {
            Value = false;
            NotifyObserver();
        }
    }
}