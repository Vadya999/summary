using Kuhpik;
using TMPro;
using UnityEngine;

public class ElevatorScreen : UIScreen
{
    [SerializeField] private TMP_Text _starCount;
    [SerializeField] private TMP_Text _level;

    public void SetStarCount(int ammount)
    {
        _starCount.text = $"{ammount}";
    }

    public void SetLevelText(int text)
    {
        _level.text = $"LEVEL {text}";
    }
}