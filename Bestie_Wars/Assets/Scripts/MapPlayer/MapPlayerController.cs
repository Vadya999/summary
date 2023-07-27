using System;
using EventBusSystem;
using Kuhpik;
using StateMachine;
using StateMachine.Conditions;
using UnityEngine;
using UnityEngine.AI;

public class MapPlayerController : MonoBehaviour
{
    [SerializeField] private MapPlayerConfiguration m_mapPlayerConfiguration;
    private NavMeshAgent navMeshAgent;
    public event Action mapPlayerDestroy;

    private void Update()
    {
        StateMachine?.Tick();
    }

    private global::StateMachine.StateMachine StateMachine;

    public void SetTarget(Transform targetCar, Transform targetExit,Podium podium)
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        var animator = new MapPlayerAnimatorController(GetComponentInChildren<Animator>());
        var idleState = new MapPlayerIdle(animator, m_mapPlayerConfiguration, transform, podium);
        var moveToFirstTarget = new MapPlayerMoveToPosition(navMeshAgent, animator);
        moveToFirstTarget.SetTarget(targetCar);
        var moveToExitTarget = new MapPlayerMoveToPosition(navMeshAgent, animator);
        moveToExitTarget.SetTarget(targetExit);
        moveToFirstTarget.AddTransition(new StateTransition(idleState,
            new FuncCondition(() => moveToFirstTarget.IsOnPosition)));
        idleState.AddTransition(new StateTransition(moveToExitTarget, new TemporaryAndFuncCondition(() => true, 10f)));
        moveToExitTarget.AddTransition(new StateTransition(idleState, new FuncCondition(() =>
        {
            if (moveToExitTarget.IsOnPosition)
            {
                mapPlayerDestroy?.Invoke();
                Destroy(gameObject);
                return true;
            }

            return false;
        })));

        StateMachine = new global::StateMachine.StateMachine(moveToFirstTarget);
    }
}