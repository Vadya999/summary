using UnityEngine;

public class ItemBubble : MonoBehaviour
{
    private float _yOffset;
    private Vector3 _initLocalPosition;

    private void Awake()
    {
        _yOffset = transform.localPosition.y;
        _initLocalPosition = transform.localPosition;
        ResetScale();
    }

    private void ResetScale()
    {
        var parent = transform.parent;
        transform.SetParent(null);
        transform.localScale = Vector3.one * 0.5f;
        transform.SetParent(parent);
    }

    private void Update()
    {
        transform.localPosition = _initLocalPosition;
        var position = transform.position;
        position.y = (transform.parent.position.y + _yOffset);
        transform.position = position;
    }
}
