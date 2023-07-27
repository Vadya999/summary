using Kuhpik;
using UnityEngine;

[CreateAssetMenu(menuName = "Configurations/Quest/QuestOpenNewPress", fileName = "QuestOpenNewPress",
    order = 0)]
public class BuyNewPress : TaskQuest
{
    [SerializeField] private float neededLevel;
    [SerializeField] private string processDescription;

    private PlayerData playerData;

    public override float ProcessDescription()
    {
        return Bootstrap.Instance.PlayerData.unlockZone.Contains(77) ? 1 : 0;
    }

    public override void EnableTask()
    {
    }

    public override void DisableTask()
    {
    }

    public override bool IsTaskCompleted()
    {
        return Bootstrap.Instance.PlayerData.unlockZone.Contains(77);
    }
}