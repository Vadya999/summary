using System;
using System.Threading.Tasks;
using Kuhpik;
using MoreMountains.NiceVibrations;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.Components
{
    public class SettingsComponent : MonoBehaviour
    {
        [SerializeField] private Animator settingAnimator;
        [SerializeField] private Button settingButton;
        [SerializeField] private Button hapticButton;
        [SerializeField] private GameObject hapticOff;

        private bool settingsIsOpen;

        private void Start()
        {
            settingButton.onClick.AddListener(SettingsClick);
            hapticButton.onClick.AddListener(HapticClick);
            SetVibrateStatus();
        }

        private async void SetVibrateStatus()
        {
            await Task.Delay(1000);
            MMVibrationManager.SetHapticsActive(Bootstrap.Instance.PlayerData.haptic);
        }

        private void SettingsClick()
        {
            settingAnimator.SetTrigger(settingsIsOpen ? "Close" : "Open");
            settingsIsOpen = !settingsIsOpen;
            if (settingsIsOpen) UpdateView();
        }

        private void HapticClick()
        {
            Bootstrap.Instance.PlayerData.haptic = !Bootstrap.Instance.PlayerData.haptic;
            Bootstrap.Instance.SaveGame();
            MMVibrationManager.SetHapticsActive(Bootstrap.Instance.PlayerData.haptic);
            UpdateView();
        }

        private void UpdateView()
        {
            var haptic = Bootstrap.Instance.PlayerData.haptic;
            hapticOff.SetActive(!haptic);
        }
    }
}