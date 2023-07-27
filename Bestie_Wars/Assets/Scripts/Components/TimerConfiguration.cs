using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class TimerConfiguration
{
    [SerializeField] private Image image;
    [SerializeField] private Image image2;
    [SerializeField] private TMP_Text text;

    public Image Image => image;
    public Image Image2 => image2;

    public TMP_Text Text => text;
}