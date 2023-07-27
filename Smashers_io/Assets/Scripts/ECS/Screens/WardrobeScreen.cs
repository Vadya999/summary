using Kuhpik;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WardrobeScreen : UIScreen
{
    [field: SerializeField] public TMP_Text hideText { get; private set; }

    [SerializeField] private Button _enterButton;
    [SerializeField] private GameObject _root;

    public Button enterButton => _enterButton;

    public UnityEvent WardrobeButtonClicked => _enterButton.onClick;

    public void SetState(bool state)
    {
        _root.SetActive(state);
    }
}
