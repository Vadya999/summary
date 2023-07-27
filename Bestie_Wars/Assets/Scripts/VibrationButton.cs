using System.Collections;
using Kuhpik;
using MoreMountains.NiceVibrations;
using UnityEngine;
using UnityEngine.UI;

public class VibrationButton : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Sprite enable;
    [SerializeField] private Sprite disable;

    public void UpdateIcon()
    {
        image.sprite = Bootstrap.Instance.PlayerData.IsVibrationActivate ? enable : disable;
    }

    public void UpdateButton()
    {
        Bootstrap.Instance.PlayerData.IsVibrationActivate = !Bootstrap.Instance.PlayerData.IsVibrationActivate;
        MMVibrationManager.SetHapticsActive(Bootstrap.Instance.PlayerData.IsVibrationActivate);
        UpdateIcon();
    }
}