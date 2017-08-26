using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Landau {
    public class UIManager : MonoBehaviour {
        static private UIManager _instance;

        private Button _startButton;
        private Button _stopButton;

        private ControlUnit _controlUnit;

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

            Main.Instance().SetUIManager(this);
        }

        // Use this for initialization
        void Start() {
            Main.Instance().ControlUnit().Protocol.Connected += ControlUnitStateChanged;
            Main.Instance().ControlUnit().Protocol.Disconnected += ControlUnitStateChanged;

            _startButton = GameObject.Find("StartControlButton").GetComponent<Button>();
            Assert.IsNotNull(_startButton);
            _stopButton = GameObject.Find("StopControlButton").GetComponent<Button>();
            Assert.IsNotNull(_stopButton);
        }

        public void ToggleControl()
        {
            if (Main.Instance().ControlUnit()._running)
                Main.Instance().ControlUnit().StartUnit();
            else
                Main.Instance().ControlUnit().StopUnit();

            UpdateButtonsState();
        }
        public void UpdateButtonsState()
        {
            ProtocolState state = Main.Instance().ControlUnit().Protocol.State;
            String text = "";
            if (state != ProtocolState.ConnectedState)
            {
                text = "CU offline";
                _startButton.interactable = false;
            }
            else if (Main.Instance().ControlUnit()._running)
            {
                text = "Stop";
                _startButton.interactable = false;
            }
            else
            {
                text = "Start";
                _startButton.interactable = true;
            }

            _startButton.GetComponentInChildren<Text>().text = text;
        }

        private void ControlUnitStateChanged(object sender, EventArgs e)
        {
            UpdateButtonsState();
        }
    }
}