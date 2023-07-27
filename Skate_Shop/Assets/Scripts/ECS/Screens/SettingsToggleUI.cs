using Kuhpik;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SettingsToggleUI : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Sprite _activeSprite;
    [SerializeField] private Sprite _inactiverSprite;
    [SerializeField] private Button _button;

    public UnityEvent onClick => _button.onClick;

    private bool _active;
    private bool active
    {
        get => _active;
        set
        {
            _active = value;
            _image.sprite = _active ? _activeSprite : _inactiverSprite;
        }
    }

    public void SetState(bool value)
    {
        active = value;
    }
}
