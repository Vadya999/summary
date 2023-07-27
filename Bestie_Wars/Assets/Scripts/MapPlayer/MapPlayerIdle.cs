using DG.Tweening;
using EventBusSystem;
using Kuhpik;
using StateMachine;
using UnityEngine;

public class MapPlayerIdle : State
{
    private readonly MapPlayerAnimatorController m_mapPlayerAnimatorController;
    private MapPlayerConfiguration m_mapPlayerConfiguration;
    private Transform m_transform;
    private readonly Podium m_podium;

    public MapPlayerIdle(MapPlayerAnimatorController mapPlayerAnimatorController,
        MapPlayerConfiguration mapPlayerConfiguration, Transform transform, Podium podium)
    {
        m_mapPlayerAnimatorController = mapPlayerAnimatorController;
        m_mapPlayerConfiguration = mapPlayerConfiguration;
        m_transform = transform;
        m_podium = podium;
    }


    public override void OnStateEnter()
    {
        m_mapPlayerAnimatorController.PlayAnimation(MapPlayerAnimationType.Idle, true);
        m_transform.DOLookAt(m_podium.randomCarPosition.position, 0.6f);
    }

    public override void OnStateExit()
    {
        m_mapPlayerAnimatorController.PlayAnimation(MapPlayerAnimationType.Idle, false);
        Bootstrap.Instance.GetSystem<MoneySpawnSystem>()
            .SpawnMoney(m_transform, 1, m_mapPlayerConfiguration.AmountMoney,m_transform);
    }
}