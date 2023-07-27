using Kuhpik;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SettingScreen : UIScreen
{
    [SerializeField] private Image _soundImage;

    [SerializeField] private Sprite _activeSprite;
    [SerializeField] private Sprite _inactiveSprite;

    [SerializeField] private Button _button;

    public UnityEvent SoundButtonClicked => _button.onClick;

    public void SetState(bool state)
    {
        _soundImage.sprite = state ? _activeSprite : _inactiveSprite;
    }
}
