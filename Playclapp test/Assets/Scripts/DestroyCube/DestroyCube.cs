using System;
using Cube;
using UnityEngine;

namespace DestroyCube
{
    public class DestroyCube : MonoBehaviour
    {
        [SerializeField] private CubeMover _cubeMover;
        [SerializeField] private ParticleSystem puffEffect;

        private void OnEnable()
        {
            _cubeMover.DestroyCubeAction.AddListener(OnTravelEndCube);
        }

        private void OnTravelEndCube()
        {
            Instantiate(puffEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            
        }

        private void OnDestroy()
        {
            _cubeMover.DestroyCubeAction.RemoveListener(OnTravelEndCube);
        }
    }
}