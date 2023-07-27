using DG.Tweening;
using Kuhpik;
using System;
using System.Collections;
using UnityEngine;

public class PlayerWalletComponent : MonoBehaviour
{
    [SerializeField] private GameObject _moneyPrefab;
    [SerializeField] private float _moneyMovementSpeed;

    public void ShowMoneyTransition(Transform target, Action onComplete = null)
    {
        var player = GameData.player.transform;
        var money = Instantiate(_moneyPrefab, player.position + Vector3.up, Quaternion.identity, transform);

        StartCoroutine(TransitionRoutine());

        IEnumerator TransitionRoutine()
        {
            yield return money.transform.DOMove(target.position, _moneyMovementSpeed).WaitForCompletion();
            Destroy(money);
            onComplete?.Invoke();
        }
    }
}