using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class PanelConfiguration
{
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private Image panel;
    [SerializeField] private Sprite enable;
    [SerializeField] private Sprite disable;

    public void SetActivate(bool isActivate)
    {
        panel.sprite = isActivate ? enable : disable;
    }

    public void SetParam(string level, string price)
    {
        levelText.text = "lvl "+level;
        priceText.text = price;
    }
}