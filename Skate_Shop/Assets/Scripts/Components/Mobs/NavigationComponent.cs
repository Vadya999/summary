using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavigationComponent : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;

    public NavMeshAgent agent => _navMeshAgent;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.avoidancePriority -= Random.Range(0, 5);
    }

    public void Warp(Vector3 point)
    {
        _navMeshAgent.Warp(point);
    }

    public void Warp(Transform point)
    {
        _navMeshAgent.Warp(point.position);
    }

    public IEnumerator MoveToPointRoutine(Vector3 point, float minDist)
    {
        _navMeshAgent.SetDestination(point);
        yield return new WaitForSeconds(0.25f);
        yield return new WaitForNavMesh(_navMeshAgent, minDist, point);
    }

    public void SetExitSpeed()
    {
        _navMeshAgent.speed *= 1.25f;
    }
}

public class WaitForNavMesh : CustomYieldInstruction
{
    private NavMeshAgent _agent;
    private Vector3 _target;

    public WaitForNavMesh(NavMeshAgent agent, float minDist, Vector3 target)
    {
        _agent = agent;
        _agent.stoppingDistance = minDist;
        _target = target;
    }

    public override bool keepWaiting
    {
        get
        {
            var distance = Vector3.Distance(_target, _agent.transform.position);
            var completed = distance <= _agent.stoppingDistance;
            if (_agent.pathStatus == NavMeshPathStatus.PathInvalid)
            {
                Debug.LogWarning($"FailedPath:{_agent.gameObject.name}");
            }
            return !completed;
        }
    }
}
