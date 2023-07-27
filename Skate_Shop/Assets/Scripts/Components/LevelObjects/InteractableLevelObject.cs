using UnityEngine;
using UnityTools;
using UnityTools.Extentions;
using UnityTools.UI;

public abstract class InteractableLevelObject : MonoBehaviour
{
    [SerializeField] private Timer _useTimer;
    [SerializeField] private ProgressBarBase _progressBar;

    public Timer useTimer => _useTimer;

    private bool _isPlayerInTrigger;

    private void Awake()
    {
        _progressBar.SetProgress(0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.HasComponent<PlayerComponent>()) _isPlayerInTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.HasComponent<PlayerComponent>()) _isPlayerInTrigger = false;
    }

    private void Update()
    {
        if (!enabled) _isPlayerInTrigger = false;
        if (!_isPlayerInTrigger || !CanInteract())
        {
            _useTimer.Reset();
        }
        else
        {
            _useTimer.UpdateTimer();
            if (_useTimer.isReady)
            {
                Interact();
                _useTimer.Reset();
            }
        }
        _progressBar.SetProgress(_useTimer.normalized);
    }

    protected abstract bool CanInteract();
    protected abstract void Interact();
}
