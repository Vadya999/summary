using System;
using System.Linq;

namespace Tutorial.Steps
{

    [Serializable]
    public class WardrobeStep : TutorialStep
    {
        private WardrobeEnterSystem _wardrobeEnterSystem;
        private WardrobeScreen _wardrobeScreen;

        public override void Enter()
        {
            _wardrobeScreen = GetScreen<WardrobeScreen>();
            _wardrobeEnterSystem = GetSystem<WardrobeEnterSystem>();
            SetPointerTarget(gameData.activeRoom.wardrobes.First());
            AddUIPointer(_wardrobeScreen.enterButton);
        }

        public override void Update()
        {
            if (_wardrobeEnterSystem.inWardrobe)
            {
                Complete();
            }
        }

        public override void Exit()
        {
            RemoveUIPointer(_wardrobeScreen.enterButton);
            SetPointerTarget(null);
        }
    }
}
