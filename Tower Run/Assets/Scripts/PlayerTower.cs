using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerTower : MonoBehaviour
{
    [SerializeField] private Human _startHuman;
        [SerializeField] private Transform _distanceChecker;
    [SerializeField] private float _fixationMaxDistance;
    [SerializeField] private BoxCollider _checkCollider;

    private List<Human> _humans;

    private void Start()
    {
        _humans = new List<Human>();
        Vector3 startPoint = transform.position;
        _humans.Add(Instantiate(_startHuman,startPoint,Quaternion.identity, transform));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Human human))
        {
            Tower collisionTower = human.GetComponentInParent<Tower>();
            if (collisionTower !=null)
            {
                List<Human> collectHumans = collisionTower.CollectHuman(_distanceChecker, _fixationMaxDistance);
                            if (collectHumans != null)
                            {
                                for (int i = collectHumans.Count -1; i >= 0 ; i--)
                                {
                                    Human insertHuman = collectHumans[i];
                                    InsertHuman(insertHuman);
                                    DisplaceCheckers(insertHuman);
                                }
                            }
                            collisionTower.Break();
            }
            
        }
    }

    private void InsertHuman(Human collectedHuman)
    {
        _humans.Insert(0,collectedHuman);
        SetHumanPosition(collectedHuman);
    }

    private void SetHumanPosition(Human human)
    {
        human.transform.parent = transform;
        human.transform.localPosition = new Vector3(0,human.transform.localPosition.y, 0);
        human.transform.localRotation = Quaternion.identity;
    }

    private void DisplaceCheckers(Human human)
    {
        float displaceScale = 1.5f;
        Vector3 distanceCheckerNewPosition = _distanceChecker.position;
            distanceCheckerNewPosition.y -= human.transform.localScale.y * displaceScale;
            _distanceChecker.position = distanceCheckerNewPosition;
            _checkCollider.center = _distanceChecker.localPosition;
    }
}
