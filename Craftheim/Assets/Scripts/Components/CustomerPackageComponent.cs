using System;
using MoreMountains.NiceVibrations;
using UnityEngine;

namespace Source.Scripts.Components
{
    public class CustomerPackageComponent : MonoBehaviour
    {
        [field:SerializeField] public Transform PackagePos { get; private set; }
        [field:SerializeField] public Transform Package { get; private set; }
        [field:SerializeField] public ZoneUIComponent Zone { get; private set; }

        public event Action OnMovedToPackage;

        public void MovedToPackage()
        {
            OnMovedToPackage?.Invoke();          
            MMVibrationManager.Haptic(HapticTypes.Selection);
        }

        public void InZone(bool value)
        {
            Zone.InZone(value);
        }
    }
}