using Kuhpik;
using UnityEngine;

public class UnlockNextLevelEventLevel : UnlockPayZoneAction
{
    [SerializeField] private LevelSegmentComponent _levelSegmentToUnlock;
    [SerializeField] private ExitComponent _exitToUnlock;
    [SerializeField] private ParticleSystem _unlockParticles;
    [SerializeField] private Transform _unlockParticlesPoint;
    [SerializeField] private BoxCollider _collider;
    [SerializeField] private int _id;

    public override void Init()
    {
        _collider.enabled = true;
    }

    public override void Invoke(UnlockPayZoneComponent zone)
    {        
        LogEvents();
        if (Bootstrap.Instance.GameData.segmentID < _id) Bootstrap.Instance.GameData.segmentID = _id;
        _collider.enabled = false;
        _exitToUnlock.ShowAnimaiton();
        SetSiblingIndex(zone);
        _levelSegmentToUnlock.gameObject.SetActive(true);
        Instantiate(_unlockParticles, _unlockParticlesPoint.position, Quaternion.identity, null);
        Destroy(zone.gameObject);
    }

    private void SetSiblingIndex(UnlockPayZoneComponent zone)
    {
        var index = zone.transform.GetSiblingIndex();
        _exitToUnlock.transform.parent = zone.transform.parent;
        _exitToUnlock.transform.SetSiblingIndex(index);

        zone.transform.SetSiblingIndex(zone.transform.parent.childCount);
    }

    private void LogEvents()
    {
        var gameData = Bootstrap.Instance.GameData;
        if (gameData.saveData != null)
        {
            if (gameData.saveData.segmentID < _id) Log();
        }
        else Log();

        void Log()
        {
            SDKEvents.progression.LevelCompleted(_id);
            SDKEvents.progression.LevelStarted(_id + 1);
        }
    }
}
