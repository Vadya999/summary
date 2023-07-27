using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityTools.Helpers;

[SelectionBase]
public class PopupComponent : MonoBehaviour
{
    [SerializeField] private TMP_Text _payAmmount;

    [SerializeField] private float _movementY;
    [SerializeField] private float _movemetDuration;

    [SerializeField] private float _appearsDuration;
    [SerializeField] private float _disappeasDuration;

    private Vector3 _initScale;

    private void Awake()
    {
        _initScale = transform.localScale;
    }

    public void Show(int ammount)
    {
        _payAmmount.text = ammount >= 0 ? $"+${ammount}" : $"${ammount}";
        if (ammount <= 0) _payAmmount.color = Color.red;
        transform.localScale = TweenHelper.zeroSize;
        StartCoroutine(AnimationRoutine());
    }

    private IEnumerator AnimationRoutine()
    {
        yield return transform.DOScale(_initScale, _appearsDuration).WaitForCompletion();
        yield return transform.DOMove(transform.position + Vector3.up * _movementY, _movemetDuration).WaitForCompletion();
        yield return transform.DOScale(TweenHelper.zeroSize, _disappeasDuration).WaitForCompletion();
        Destroy(gameObject);
    }
}
