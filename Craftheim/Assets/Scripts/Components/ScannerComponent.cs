using System;
using Kuhpik;
using MoreMountains.NiceVibrations;
using UnityEngine;

namespace Source.Scripts.Components
{
    public class ScannerComponent : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private ZoneUIComponent zone;

        public event Action OnScanning;

        public void Scanning()
        {
            MMVibrationManager.Haptic(HapticTypes.Selection);
            OnScanning?.Invoke();
            animator.SetTrigger("Scan");
        }

        public void InZone(bool value)
        {
            zone.InZone(value);
        }
    }
}