using Kuhpik;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LoseScreen : UIScreen
{
    [SerializeField] private Button _retryButton;
    [SerializeField] private GameObject _root;

    public UnityEvent RetryButtonClicked => _retryButton.onClick;

    public void Show()
    {
        _root.SetActive(true);
    }
}