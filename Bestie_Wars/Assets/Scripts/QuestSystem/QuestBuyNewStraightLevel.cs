using Kuhpik;
using UnityEngine;

[CreateAssetMenu(menuName = "Configurations/Quest/QuestBuyNewStraightLevel", fileName = "QuestBuyNewStraightLevel",
    order = 0)]
public class QuestBuyNewStraightLevel : TaskQuest
{
    [SerializeField] private float neededLevel;
    [SerializeField] private string processDescription;

    private PlayerData playerData;

    public override float ProcessDescription()
    {
        if (Bootstrap.Instance.PlayerData.UpgadeLevel[UpgradeType.CarAttachLevel] / neededLevel > 1)
        {
            return 1;
        }

        return ((float) Bootstrap.Instance.PlayerData.UpgadeLevel[UpgradeType.CarAttachLevel]) / neededLevel;
    }

    public override void EnableTask()
    {
    }

    public override void DisableTask()
    {
    }

    public override bool IsTaskCompleted()
    {
        return Bootstrap.Instance.PlayerData.UpgadeLevel[UpgradeType.CarAttachLevel] >= neededLevel;
    }
}