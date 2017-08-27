using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Landau {
    public class UIManager : MonoBehaviour {
        static private UIManager _instance;

        private Button _startButton;
        private Text _statusText;

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
            Main.Instance().ControlUnit().Protocol.Connected += StateChanged;
            Main.Instance().ControlUnit().Protocol.Disconnected += StateChanged;
            Main.Instance().SensorsMgr().Protocol.Connected += StateChanged;
            Main.Instance().SensorsMgr().Protocol.Disconnected += StateChanged;

            _startButton = GameObject.Find("StartControlButton").GetComponent<Button>();
            Assert.IsNotNull(_startButton);
            _statusText = GameObject.Find("StatusText").GetComponent<Text>();
            Assert.IsNotNull(_statusText);

            UpdateUiState();
        }

        public void ToggleControl()
        {
            if (Main.Instance().ControlUnit()._running)
                Main.Instance().ControlUnit().StartUnit();
            else
                Main.Instance().ControlUnit().StopUnit();

            UpdateUiState();
        }

        public void UpdateUiState()
        {
            ProtocolState state = Main.Instance().ControlUnit().Protocol.State;
            String text = "ControlUnit: ";
            if (state != ProtocolState.ConnectedState)
            {
                text += "Not connected";
            }
            else
            {
                text += "Connected";
            }

            state = Main.Instance().SensorsMgr().Protocol.State;
            text += "\nSensorsMgr: ";
            if (state != ProtocolState.ConnectedState)
            {
                text += "Not connected";
            }
            else
            {
                text += "Connected";
            }
            _statusText.text = text;

            if (Main.Instance().ControlUnit()._running)
            {
                _startButton.GetComponentInChildren<Text>().text = "Stop";
                _startButton.interactable = false;
            }
            else
            {
                _startButton.GetComponentInChildren<Text>().text = "Start";
                _startButton.interactable = true;
            }
        }

        private void StateChanged(object sender, EventArgs e)
        {
            UpdateUiState();
        }
    }
}