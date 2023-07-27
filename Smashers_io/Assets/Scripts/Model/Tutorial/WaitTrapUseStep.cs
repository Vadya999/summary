using System;
using UnityEngine;
using UnityTools.Extentions;

namespace Tutorial.Steps
{
    [Serializable]
    public class WaitTrapUseStep : TutorialStep
    {
        [SerializeField] private bool _disableWardrobeButton;

        private WardrobeScreen _wardrobeScreen;

        public override void Enter()
        {
            _wardrobeScreen = GetScreen<WardrobeScreen>();
            if (_disableWardrobeButton)
            {
                _wardrobeScreen.enterButton.SetButtonState(false);
            }
        }

        public override void Exit()
        {
            if (_disableWardrobeButton)
            {
                _wardrobeScreen.enterButton.SetButtonState(true);
            }
        }

        public override void Update()
        {
            foreach (var trap in gameData.activeRoom.traps)
            {
                if (trap.canUse)
                {
                    return;
                }
            }
            Complete();
        }
    }
}
