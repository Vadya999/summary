using System;
using System.Collections.Generic;
using DG.Tweening;
using FactoryPool;
using Kuhpik;
using UnityEngine;
using Random = UnityEngine.Random;

public class MoneySpawnSystem : GameSystem
{
    [SerializeField] private CarUpgradeConfiguration carUpgradeConfiguration;
    [SerializeField] private Money moneyPrefab;
    [SerializeField] private Transform positionMoney;
    [SerializeField] private Transform positionMoneyMapCharacter;
    [SerializeField] private float radius;

    private IPool<Money> moneyPool;

    private void Awake()
    {
        var factory = new FactoryMonoObject<Money>(moneyPrefab.gameObject, transform);
        moneyPool = new Pool<Money>(factory, 3);
    }

    public void SpawnMoney(Transform position, int amount, int addMoney, Transform spawnPos = null)
    {
        Debug.Log(addMoney);
        Debug.Log(Convert.ToInt32(addMoney *
                                  (player.UpgadeLevel[UpgradeType.IncomeLevel - 1] *
                                   carUpgradeConfiguration.AddMoneyPerLvl)));
        addMoney = addMoney + Convert.ToInt32(addMoney *
                                              (player.UpgadeLevel[UpgradeType.IncomeLevel - 1] *
                                               carUpgradeConfiguration.AddMoneyPerLvl));
        var spawnedMoney = new List<Money>();
        var currentAdd = 0;
        for (int i = 0; i < amount; i++)
        {
            spawnedMoney.Add(moneyPool.Pull());
        }

        var add = addMoney / amount;
        foreach (var money in spawnedMoney)
        {
            money.SetMoney(add);
            money.Rigidbody.isKinematic = true;
            money.transform.position = position.position;
            money.transform.rotation = Quaternion.Euler(new Vector3(Random.Range(-360f, 360f),
                Random.Range(-360f, 360f), Random.Range(-360f, 360f)));
            var currentPos = spawnPos == null ? positionMoney : spawnPos;
            var pos = currentPos.position + new Vector3(Random.Range(-radius / 2f, radius / 2f), 0f,
                Random.Range(-radius / 2f, radius / 2f));
            if (spawnPos != null) pos = spawnPos.position;
            money.transform.DOJump(pos, Random.Range(1f, 1.5f), 1, Random.Range(1f, 1.5f))
                .OnComplete(() => money.Rigidbody.isKinematic = false);
            currentAdd += add;
        }

        if (currentAdd != addMoney)
        {
            if (currentAdd > addMoney)
            {
                while (currentAdd > addMoney)
                {
                    currentAdd--;
                    spawnedMoney[Random.Range(0, spawnedMoney.Count)].SetMoney(add - 1);
                }
            }
            else
            {
                while (currentAdd < addMoney)
                {
                    currentAdd++;
                    spawnedMoney[Random.Range(0, spawnedMoney.Count)].SetMoney(add + 1);
                }
            }
        }
    }
}