using Kuhpik;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RoomEnterScreen : UIScreen
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private GameObject _root;
    [SerializeField] private Button _enterButton;

    public Button enterButton => _enterButton;

    public UnityEvent EnterButtonClicked => _enterButton.onClick;

    public void SetState(bool state, string text)
    {
        _text.text = text;
        _root.SetActive(state);
    }
}
