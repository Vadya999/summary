using Kuhpik;
using UnityEngine;
using UnityEngine.UI;
using UnityTools.Extentions;

public class TargetPointerScreen : UIScreen
{
    [SerializeField] private Image _arrow;
    [SerializeField] private Image _icon;

    public bool isShowingArrow { set => _arrow.gameObject.SetActive(value); }

    public void UpdateArrow(Quaternion rotation, Vector3 direction, float distance, bool onEdge)
    {
        _arrow.transform.rotation = rotation;

        if (!onEdge)
        {
            _arrow.transform.localPosition = new Vector3(direction.x, direction.z, 0).normalized * distance.Clamp(0, 20).Remap(0, 20, 100, 350);
        }
        else
        {
            var h = (Screen.height / 2) - _arrow.rectTransform.sizeDelta.x;
            var w = (Screen.width / 2) - _arrow.rectTransform.sizeDelta.y;

            var localDirection = new Vector3(direction.x, direction.z, 0).normalized;
            localDirection *= h;
            localDirection.x = localDirection.x.Clamp(-w, w);
            localDirection.y = localDirection.y.Clamp(-h, h);
            _arrow.transform.localPosition = localDirection;
        }
    }

    public void SetSprite(Sprite sprite)
    {
        _icon.SetSpriteWithoutBackground(sprite);
    }
}
