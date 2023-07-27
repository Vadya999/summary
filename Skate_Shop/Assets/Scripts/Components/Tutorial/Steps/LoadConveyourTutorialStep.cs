using Kuhpik;
using System;

[Serializable]
public class LoadConveyourTutorialStep : TutorialStep
{
    private BoxStackComponent boxStack => GameData.player.boxRoot;

    public override void Enter()
    {
        boxStack.Changed.AddListener(OnBoxCountChanged);
        OnBoxCountChanged();
    }

    public override void Exit()
    {
        boxStack.Changed.RemoveListener(OnBoxCountChanged);
    }

    private void OnBoxCountChanged()
    {
        if (!boxStack.hasItem)
        {
            Complete();
        }
    }
}
