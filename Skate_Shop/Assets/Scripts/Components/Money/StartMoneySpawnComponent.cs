using Kuhpik;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StartMoneySpawnComponent : MonoBehaviour
{
    [SerializeField] private MoneyComponent _moneyPrefab;
    [SerializeField] private float _collectDelay;

    private int _cost;

    private Vector3 _moneySize = new Vector3(0.65f, 0.2f, 0.35f);
    private Quaternion _moneyRotation = Quaternion.Euler(-90, 0, 0);

    private Vector3Int _moneyCount = new Vector3Int(4, 2, 5);

    private List<MoneyComponent> _moneys = new List<MoneyComponent>(80);

    private void Start()
    {
        CalculateCost();
        Spawn();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerComponent player))
        {
            Collect(player);
        }
    }

    private void Collect(PlayerComponent player)
    {
        _moneys.ForEach(money => money.StartCoroutine(CollectRoutine(player, money)));
        GameData.walletModel.moneyCount += _cost;
        Destroy(gameObject);
    }

    private IEnumerator CollectRoutine(PlayerComponent player, MoneyComponent money)
    {
        var delay = Vector3.Distance(player.transform.position, money.transform.position) * _collectDelay;
        money.transform.parent = null;
        yield return new WaitForSeconds(delay);
        money.TranslateMoneyToPlayer(player.transform);
    }

    private void Spawn()
    {
        for (int x = 0; x < _moneyCount.x; x++)
        {
            for (int y = 0; y < _moneyCount.y; y++)
            {
                for (int z = 0; z < _moneyCount.z; z++)
                {
                    var position = transform.position + new Vector3(x * _moneySize.x, y * _moneySize.y, z * _moneySize.z);
                    var clone = Instantiate(_moneyPrefab, position, _moneyRotation, transform);
                    clone.cost = 0;
                    clone.GetComponent<Collider>().enabled = false;
                    _moneys.Add(clone);
                }
            }
        }
    }

    private void CalculateCost()
    {
        _cost = FindObjectsOfType<UnlockPayZoneComponent>()
            .OrderBy(x => x.transform.GetSiblingIndex())
            .Take(2)
            .Select(x => x.cost)
            .Sum();
    }

}
