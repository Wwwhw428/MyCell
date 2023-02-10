using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MyCell.Managers
{
    public class CriticalManager : MonoBehaviour
    {
        public event Action test;
        // public int SlowFrame;
        public float SlowTime;
        public float TimeScale;
        private bool _isSlow;
        // private int _slowFrame;
        private float _startTime;

        private void Update() {
            if (_isSlow)
            {
                if (Time.time - _startTime >= SlowTime)
                {
                    _isSlow = false;
                    Time.timeScale = 1;
                }
            }
        }

        public void CriticalHandler(GameObject obj)
        {
            // TODO: 连续多次暴击则只有第一次暴击时间变慢
            // TODO: 这里写的有点麻烦
            obj.transform.Find("Core/DamageComponent").SendMessage("Critical");

            // _slowFrame = SlowFrame;
            _startTime = Time.time;
            _isSlow = true;
            Time.timeScale = TimeScale;
        }
    }
}
