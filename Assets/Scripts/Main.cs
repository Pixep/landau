using UnityEngine;

namespace Landau {
    public class Main : MonoBehaviour {

        static Main _instance;
        static UIManager _uiManager;

        static public Main Instance() { return _instance; }
        static public MainFactory Factory = new MainFactory();
        static public UIManager UIManager() { return _uiManager; }

        public ControlUnit ControlUnit() { return _controlUnit; }
        public SensorsManager SensorsMgr() { return _sensorsManager; }

        private SensorsManager _sensorsManager;
        private ControlUnit _controlUnit;

        // Use this for initialization
        void Awake() {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Debug.Log("Main already instantiated");
                Destroy(this);
            }

            Init();
        }

        void Init()
        {
            gameObject.AddComponent(typeof(UnityMainThreadDispatcher));
            _sensorsManager = SensorsManager.Instance();
        }

        public void SetUIManager(UIManager manager)
        {
            _uiManager = manager;
        }

        public void SetControlUnit(ControlUnit controlUnit)
        {
            _controlUnit = controlUnit;
        }
    }
}