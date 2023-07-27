using DG.Tweening;
using Kuhpik;
using MoreMountains.NiceVibrations;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityTools.Extentions;
using UnityTools.Helpers;

public class CashRegisterComponent : MonoBehaviour
{
    [field: SerializeField] public PseudoQueue queue { get; private set; }

    [SerializeField] private PopupComponent _collectPopup;
    [SerializeField] private GameObject _moneyPrefab;
    [SerializeField] private Transform _moneyStackRoot;

    private List<GameObject> _moneyStack = new List<GameObject>();

    private int _totalMoney;

    private void OnTriggerStay(Collider other)
    {
        if (other.HasComponent<PlayerComponent>() && _totalMoney > 0)
        {
            CollectMoney();
        }
    }

    public void AddMoney(int money, MobAI mob)
    {
        _totalMoney += money;
        for (int i = 0; i < 2; i++)
        {
            StartCoroutine(IncreaseMoneyStack(mob.transform.position));
        }
    }

    private void CollectMoney()
    {
        var temp = _totalMoney;
        MoneyCollect();
        this.Invoke(() =>
        {
            GameData.walletModel.moneyCount += temp;
            ShowPopup(temp);
            if (GameData.settings.hapticEnabled) MMVibrationManager.Haptic(HapticTypes.LightImpact);
        }, 0.5f);

    }

    private void MoneyCollect()
    {
        foreach (var money in _moneyStack)
        {
            StartCoroutine(MoneyCollectRoutine(money));
        }
        _moneyStack.Clear();
        _totalMoney = 0;
    }

    private IEnumerator MoneyCollectRoutine(GameObject money)
    {
        money.transform.parent = GameData.player.transform;
        yield return new WaitForSeconds(Random.Range(0, 0.2f));
        money.transform.DOLocalJump(Vector3.zero, 1 + Random.Range(0, 1f), 1, 0.5f).WaitForCompletion();
        yield return new WaitForSeconds(0.2f);
        money.transform.DOScale(TweenHelper.zeroSize, 0.2f);
        yield return new WaitForSeconds(0.2f);
        money.transform.DOKill();
        Destroy(money);
    }

    private void ShowPopup(int ammount)
    {
        var clone = Instantiate(_collectPopup);
        clone.transform.position = GameData.player.transform.position + Vector3.up * 3;
        clone.Show(ammount);
    }

    private IEnumerator IncreaseMoneyStack(Vector3 startPosition)
    {
        var clone = Instantiate(_moneyPrefab, _moneyStackRoot);
        clone.transform.position = startPosition;
        var moneyPosition = Vector3.up * (Mathf.FloorToInt(_moneyStack.Count / 2f)) * 0.2f;
        if (_moneyStack.Count % 2 == 0) moneyPosition += _moneyStackRoot.right * 0.4f;
        _moneyStack.Add(clone);
        yield return clone.transform.DOLocalJump(moneyPosition, 1, 1, 0.5f).WaitForCompletion();
    }
}
