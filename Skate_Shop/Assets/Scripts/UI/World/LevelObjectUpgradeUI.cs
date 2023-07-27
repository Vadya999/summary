using TMPro;
using UnityEngine;

public class LevelObjectUpgradeUI : MonoBehaviour
{
    [field: SerializeField] private TMP_Text level { get; set; }
    [field: SerializeField] public TMP_Text cost { get; private set; }

    public void SetLevel(int level, int maxLevel)
    {
        if (level < maxLevel)
        {
            this.level.text = $"LVL{level + 1}";
        }
        else
        {
            this.level.text = "MAX";
        }
    }
}
