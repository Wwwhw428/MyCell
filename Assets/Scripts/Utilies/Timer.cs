using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Addon.Utilites
{
    public class Timer
    {
        private float _startTime;
        private float _duration;
        private float _targetTime;
        private bool _active;

        public event Action OnTimerDone;

        public Timer()
        {
            StopTimer();
        }

        public void SetTimer(float duration)
        {
            this._duration = duration;
        }

        public void StartTimer()
        {
            _startTime = Time.time;
            _targetTime = _startTime + _duration;
            _active = true;
        }

        public void StopTimer() => _active = false;
        
        public void Tick()
        {
            if (!_active) return;

            if (Time.time >= _targetTime)
                OnTimerDone?.Invoke();
        }
    }
}
