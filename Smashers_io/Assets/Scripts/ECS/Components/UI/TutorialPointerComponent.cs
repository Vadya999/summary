using NaughtyAttributes;
using UnityEngine;
using UnityTools.Extentions;

public class TutorialPointerComponent : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField, MinMaxSlider(0, 10)] private Vector2 _sizeRange;

    private Vector3 _minSize;
    private Vector3 _maxSize;

    private float _time;

    private void Awake()
    {
        _minSize = Vector3.one * _sizeRange.x;
        _maxSize = Vector3.one * _sizeRange.y;
    }

    private void Update()
    {
        _time += Time.deltaTime * _speed;

        var clampTime = Mathf
            .Sin(_time)
            .Remap(-1, 1, 0, 1);

        transform.localScale = Vector3.Lerp(_minSize, _maxSize, clampTime);
    }
}
