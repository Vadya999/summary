using Kuhpik;
using System;

[Serializable]
public class PickupSkatesTutorialStep : TutorialStep
{
    private SkateStackComponent skateStack => GameData.player.skatesRoot;

    public override void Enter()
    {
        skateStack.Changed.AddListener(OnBoxCountChanged);
        OnBoxCountChanged();
    }

    public override void Exit()
    {
        skateStack.Changed.RemoveListener(OnBoxCountChanged);
    }

    private void OnBoxCountChanged()
    {
        if (skateStack.hasItem)
        {
            Complete();
        }
    }
}
