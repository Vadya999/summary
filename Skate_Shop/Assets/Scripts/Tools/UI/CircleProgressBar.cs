using UnityEngine;
using UnityEngine.UI;

namespace UnityTools.UI
{
    public class CircleProgressBar : ProgressBarBase
    {
        [SerializeField] private Image _image;

        public override void SetProgress(float value)
        {
            _image.fillAmount = value;
        }
    }
}