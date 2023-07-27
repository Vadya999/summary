using System;
using HomaGames.HomaBelly;
using Kuhpik;
using Snippets.Tutorial;
using UnityEngine;

namespace Source.Scripts.Tutorial
{
    [Serializable]
    public class ScoreboardStep : TutorialStep
    {
        [SerializeField] private float delaySwitch = 3f;
        [SerializeField] private float startDelay = 2f;
        
        private float timeSwitch;
        private float timeStart;
        private bool switched;
        private bool started;
        
        public override void OnUpdate()
        {
            if (!started) return;
            timeStart -= Time.deltaTime;

            if (!switched && timeStart < 0)
            {
                Bootstrap.Instance.GameData.canMove = false;
                switched = true;
                Bootstrap.Instance.GameData.cinemachineSwitchComponent.Switch();
            }
            
            if (switched)
            {
                timeSwitch -= Time.deltaTime;
                
                if (timeSwitch < 0)
                {
                    switched = false;
                    Bootstrap.Instance.GameData.cinemachineSwitchComponent.Switch();
                    Bootstrap.Instance.GameData.canMove = true;
                    Complete();
                }
            }
        }

        protected override void OnBegin()
        {
            HomaBelly.Instance.TrackDesignEvent("tutorial_started");
            timeStart = startDelay;
            timeSwitch = delaySwitch;
            started = true;
        }

        protected override void OnComplete()
        {
            HomaBelly.Instance.TrackDesignEvent(eventKey);
        }
    }
}