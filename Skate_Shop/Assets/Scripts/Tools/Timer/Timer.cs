using System;
using UnityEngine;

namespace UnityTools
{
    [Serializable]
    public class Timer
    {
        [SerializeField] private float _coolDown;

        private float cooldDownLeft;

        public bool isReady => IsReady();
        public float left => cooldDownLeft;
        public float normalized => Normlized();

        public Timer(float coolDown)
        {
            _coolDown = coolDown;
        }

        public bool IsReady()
        {
            return cooldDownLeft < 0;
        }

        public float Normlized()
        {
            return 1 - (cooldDownLeft / _coolDown);
        }

        public void UpdateTimer()
        {
            cooldDownLeft -= Time.deltaTime;
        }

        public void UpdateTimer(float time)
        {
            cooldDownLeft -= time;
        }

        public void Reset(float newCoolDown)
        {
            _coolDown = newCoolDown;
            Reset();
        }

        public void Reset()
        {
            cooldDownLeft = _coolDown;
        }

        public void End()
        {
            cooldDownLeft = 0;
        }
    }
}