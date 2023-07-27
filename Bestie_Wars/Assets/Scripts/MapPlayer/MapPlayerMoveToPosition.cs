using StateMachine;
using UnityEngine;
using UnityEngine.AI;

public class MapPlayerMoveToPosition : State
{
    public bool IsOnPosition;

    private Transform target;
    private readonly NavMeshAgent agent;
    private readonly MapPlayerAnimatorController m_mapPlayerAnimatorController;

    public MapPlayerMoveToPosition(NavMeshAgent agent, MapPlayerAnimatorController mapPlayerAnimatorController)
    {
        this.agent = agent;
        m_mapPlayerAnimatorController = mapPlayerAnimatorController;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public override void Tick()
    {
        agent.SetDestination(target.position);
        if (Vector3.Distance(target.position, agent.transform.position) < 1.4)
        {
            IsOnPosition = true;
        }
    }

    public override void OnStateEnter()
    {
        m_mapPlayerAnimatorController.PlayAnimation(MapPlayerAnimationType.Walk, true);
        agent.speed = 1;
    }

    public override void OnStateExit()
    {
        m_mapPlayerAnimatorController.PlayAnimation(MapPlayerAnimationType.Walk, false);
        agent.speed = 0;
    }
}