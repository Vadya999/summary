using DG.Tweening;
using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;
using UnityTools.Extentions;
using UnityTools.Helpers;

public class DefualtUnlockPayZoneAction : UnlockPayZoneAction
{
    [Header("Main settings")]
    [SerializeField] private GameObject _objectToUnlock;

    [Header("Unlock chain")]
    [SerializeField] private bool _hasZoneToUnlock;
    [SerializeField, ShowIf(nameof(_hasZoneToUnlock))] private List<UnlockPayZoneComponent> _nextZone;

    [Header("Appears aimation")]
    [SerializeField] private ParticleSystem _unlockParticle;
    [SerializeField] private bool _showSpawnAnimation;
    [SerializeField, ShowIf(nameof(_showSpawnAnimation))] private float _spawnAnimationRoutine;

    public GameObject objectToUnlock => _objectToUnlock;

    public override void Init()
    {
        _objectToUnlock.SetActive(false);
        if (_hasZoneToUnlock)
        {
            _nextZone.ForEach(x => x.gameObject.SetActive(false));
        }
    }

    public override void Invoke(UnlockPayZoneComponent zone)
    {
        UnlockNextPayZones();
        ActivatePayObject();
        SetSiblingIndex(zone);
        ShowSpawnAnimation();
        ShowParticle();
        Destroy(zone.gameObject);
    }

    private void ActivatePayObject()
    {
        _objectToUnlock.SetActive(true);
    }

    private void UnlockNextPayZones()
    {
        if (_hasZoneToUnlock)
        {
            _nextZone.ForEach(x => x.gameObject.SetActive(true));
        }
    }

    private void SetSiblingIndex(UnlockPayZoneComponent zone)
    {
        var index = zone.transform.GetSiblingIndex();
        _objectToUnlock.transform.parent = zone.transform.parent;
        _objectToUnlock.transform.SetSiblingIndex(index);

        zone.transform.SetSiblingIndex(zone.transform.parent.childCount);
    }

    private void ShowSpawnAnimation()
    {
        if (_showSpawnAnimation)
        {
            var initLocalScale = _objectToUnlock.transform.localScale;
            _objectToUnlock.transform.localScale = TweenHelper.zeroSize;
            _objectToUnlock.transform.DOScale(initLocalScale, _spawnAnimationRoutine).SetEase(Ease.InOutElastic);
        }
    }

    private void ShowParticle()
    {
        Instantiate(_unlockParticle, transform.position + Vector3.up, Quaternion.identity);
    }
}
