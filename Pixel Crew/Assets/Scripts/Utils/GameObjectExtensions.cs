using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelCrew.Utils
{
    public static class GameObjectExtensions
    {
        //[SerializeField] private LayerMask _layer;
        public static bool IsInLayer(this GameObject go,LayerMask _layer)
        {
            return _layer == (_layer | 1 << go.layer);
        }    
        
    }
}

