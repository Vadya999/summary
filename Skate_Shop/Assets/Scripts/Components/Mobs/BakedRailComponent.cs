using System.Collections;
using System.Linq;
using UnityEngine;

public class BakedRailComponent : RailComponent
{
    [SerializeField] private RampAnimationComponent _fakeRoot;
    [SerializeField] private float _animationDuration;

    private void Awake()
    {
        _fakeRoot.gameObject.SetActive(false);
    }

    public override IEnumerator ShowRailAnimation(MobAI mob)
    {
        var fakeRootClone = Instantiate(_fakeRoot, _fakeRoot.transform.position, _fakeRoot.transform.rotation, _fakeRoot.transform.parent);
        fakeRootClone.gameObject.SetActive(true);
        fakeRootClone.SetSkate(mob.skate);
        mob.navigation.Warp(onRailPath.path.Last());
        mob.visualRoot.SetActive(false);
        yield return new WaitForSeconds(_animationDuration);
        mob.visualRoot.SetActive(true);
        mob.transform.rotation = Quaternion.LookRotation(transform.forward);
        Destroy(fakeRootClone.gameObject);
    }
}
