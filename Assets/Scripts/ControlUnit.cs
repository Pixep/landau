using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

namespace Landau
{
    public class ControlUnit : MonoBehaviour
    {
        public bool _running = false;
        [SerializeField]
        private CarController _carController;

        public float Steering { get; set; }
        public float Acceleration { get; set; }
        public float Brake { get; set; }
        public float HandBrake { get; set; }
        
        private IControlUnitProtocol _protocol;
        public IControlUnitProtocol Protocol { get { return _protocol; } }

        private void Awake()
        {
            Main.Instance().SetControlUnit(this);
            _protocol = Main.Factory.CreateControlProtocol(this);
        }

        // Use this for initialization
        void Start()
        {
            if (_running)
                StartUnit();
            else
                ResetUnit();
        }

        // Update is called once per frame
        void Update()
        {
            if (!_running)
                return;

            if (!_carController)
            {
                Debug.Log("No car controller set");
                StopUnit();
                return;
            }

            // Steering, Acceleration, Brake, Handbrake
            _carController.Move(Steering, Acceleration, Brake, HandBrake);
        }

        public void ResetUnit()
        {
            _running = false;
            Steering = Acceleration = Brake = HandBrake = 0;

            if (_carController)
                _carController.AutomaticControl = false;
        }

        public void StartUnit()
        {
            _running = true;

            if (_carController)
                _carController.AutomaticControl = true;
        }

        public void StopUnit()
        {
            ResetUnit();
        }
    }
}