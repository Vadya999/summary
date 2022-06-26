using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PixelCrew.Components
{
    public class SpawnListComponent : MonoBehaviour
    {
        [SerializeField] private SpawnData[] _spawners;

        public void Spawn(string id)
        {
           var spawner =  _spawners.FirstOrDefault(element => element.Id == id);
           // spawner?.Component.
           
            /*foreach (var data in _spawners)
            {
                if (data.Id == id)
                {
                    data.Component.Spawn();
                    break;
                }
            }*/
        }
        [Serializable]
        public class SpawnData
        {
            public string Id;
            [SerializeField] public SpawnData Component;
        }
    }
}

