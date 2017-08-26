using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Landau {
    public class UIManager : MonoBehaviour {
        static private UIManager _instance;

        private Button StartButton;
        private Button StopButton;

        void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Debug.Log("UIManager already instantiated");
                Destroy(this);
            }
        }

        // Use this for initialization
        void Start() {
            Main.Instance().SetUIManager(this);

            StartButton = GameObject.Find("StartControlButton").GetComponent<Button>();
            Assert.IsNotNull(StartButton);
            StopButton = GameObject.Find("StopControlButton").GetComponent<Button>();
            Assert.IsNotNull(StopButton);
        }

        public void StartAutomaticControl()
        {
            Main.Instance().ControlUnit().StartUnit();
            StartButton.interactable = false;
            StopButton.interactable = true;
        }
        public void StopAutomaticControl()
        {
            Main.Instance().ControlUnit().StopUnit();
            StartButton.interactable = true;
            StopButton.interactable = false;
        }
    }
}