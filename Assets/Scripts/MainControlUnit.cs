using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class MainControlUnit : MonoBehaviour {

    public bool m_running = false;
    [SerializeField] private CarController m_carController;

    // Use this for initialization
    void Start()
    {
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
        m_carController.Move(0, 0.5f, 0, 0);
    }

    public void StartUnit() {
        m_running = true;

        if (m_carController)
            m_carController.AutomaticControl = true;
    }

    public void StopUnit() {
        m_running = false;

        if (m_carController)
            m_carController.AutomaticControl = false;
    }
}
