using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class MainControlUnit : MonoBehaviour {

    public bool m_running = false;
    [SerializeField] private CarController m_carController;

    float Steering { get; set; }
    float Acceleration { get; set; }
    float Brake { get; set; }
    float HandBrake { get; set; }

    // Use this for initialization
    void Start()
    {
        if (m_running)
            StartUnit();
        else
            ResetUnit();
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_running)
            return;

        if (!m_carController)
        {
            Debug.Log("No car controller set");
            StopUnit();
            return;
        }

        // Steering, Acceleration, Brake, Handbrake
        m_carController.Move(Steering, Acceleration, Brake, HandBrake);
    }

    public void ResetUnit()
    {
        m_running = false;
        Steering = Acceleration = Brake = HandBrake = 0;

        if (m_carController)
            m_carController.AutomaticControl = false;
    } 

    public void StartUnit()
    {
        m_running = true;

        Acceleration = 0.5f;

        if (m_carController)
            m_carController.AutomaticControl = true;
    }

    public void StopUnit()
    {
        ResetUnit();
    }
}
