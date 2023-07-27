using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.Components
{
    public class ZoneUIComponent : MonoBehaviour
    {
        [SerializeField] private Image image;

        public void InZone(bool value)
        {
            image.color = value ? Color.green : Color.white;
        }
    }
}