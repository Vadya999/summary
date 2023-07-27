using UnityEngine;

namespace UnityTools.UI
{
    public abstract class ProgressBarBase : MonoBehaviour
    {
        public abstract void SetProgress(float value);
    }
}