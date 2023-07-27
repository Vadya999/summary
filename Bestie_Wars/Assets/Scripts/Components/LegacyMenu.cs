using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Kuhpik;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LegacyMenu : UIScreen
{
    [SerializeField] private Podium podium;
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text text;

    public override void Open()
    {
        base.Open();
        text.text= podium.AmountPodium + "/" + podium.MaxAttach;
        image.transform.DOShakeScale(0.3f, 0.4f);
        image.sprite = podium.last.carLegacySprite;
    }
}
