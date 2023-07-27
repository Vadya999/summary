using DG.Tweening;
using System.Collections;
using System.Linq;
using UnityEngine;

public class RailComponent : MonoBehaviour
{
    [field: SerializeField] public Transform entryPoint { get; private set; }
    [field: SerializeField] public PathComponent onRailPath { get; private set; }
    [field: SerializeField] public bool isRamp { get; private set; }

    public virtual IEnumerator ShowRailAnimation(MobAI mob)
    {
        mob.animation.ShowSkating();
        mob.navigation.agent.enabled = false;
        var windParticle = Instantiate(mob.windParticle, mob.skate.transform);
        yield return DoRail(mob);
        Destroy(windParticle);
        mob.navigation.agent.enabled = true;
        mob.navigation.Warp(onRailPath.path.Last());
    }

    private IEnumerator DoRail(MobAI mob)
    {
        for (int i = 0; i < onRailPath.path.Length - 1; i++)
        {
            var currentPoint = onRailPath.path[i];
            var nextPoint = onRailPath.path[i + 1];
            var duration = Vector3.Distance(currentPoint, nextPoint) / 5f;

            var lookDirection = nextPoint - currentPoint;
            lookDirection.y = 0;
            var rotation = Quaternion.LookRotation(lookDirection) * Quaternion.Euler(0, -90, 0);

            mob.transform.DORotate(rotation.eulerAngles, duration);

            yield return mob.transform
                .DOMove(nextPoint, duration)
                .SetEase(Ease.Linear)
                .WaitForCompletion();
        }
    }
}
