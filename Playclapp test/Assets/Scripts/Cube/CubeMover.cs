using DG.Tweening;
using Geniral_Settings;
using UnityEngine;
using UnityEngine.Events;

namespace Cube
{
    public class CubeMover : MonoBehaviour
    {
        public UnityEvent DestroyCubeAction;
        
        private GeniralSettings _geniralSettings;
        private Factory.Factory _factory;

        private float tralevedDistance;
        private float moveSpeed;

        private Vector3 distanceVector3;

        private void Awake()
        {
            _geniralSettings = FindObjectOfType<GeniralSettings>();

            tralevedDistance = _geniralSettings.traveledDistance;
            moveSpeed = _geniralSettings.speedCube;
        }

        private void Start()
        {
            SetDictanceVector();

            MoveCube();
        }

        private void MoveCube()
        {
            transform.DOMove(distanceVector3, moveSpeed).OnComplete(() => DestroyCubeAction?.Invoke());
        }

        private void SetDictanceVector() =>
            distanceVector3 = new Vector3(0, 0, tralevedDistance);
    }
}