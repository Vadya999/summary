using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace PixelCrew.Components
{
    public class SpawnComponent : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private GameObject _prefab;

        [ContextMenu("Spawn")]
        public void Spawn()
        {
            var instance =  Instantiate(_prefab, _target.transform.position, quaternion.identity);
            instance.transform.localScale = transform.lossyScale;//rotate to target
        } 
    }
}

