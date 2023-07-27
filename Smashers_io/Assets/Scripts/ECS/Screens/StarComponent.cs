using UnityEngine;
using UnityEngine.UI;

public class StarComponent : MonoBehaviour
{
    [SerializeField] private Sprite _activeSprite;
    [SerializeField] private Sprite _inactiveSprite;

    [SerializeField] private Image _image;

    public void SetState(bool state)
    {
        _image.sprite = state ? _activeSprite : _inactiveSprite;
    }
}
