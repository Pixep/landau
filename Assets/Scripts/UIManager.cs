﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Landau {
    public class UIManager : MonoBehaviour {
        static private UIManager _instance;

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
        }

        public void StartAutomaticControl()
        {
            Main.Instance().ControlUnit().StartUnit();
        }
        public void StopAutomaticControl()
        {
            Main.Instance().ControlUnit().StopUnit();
        }
    }
}