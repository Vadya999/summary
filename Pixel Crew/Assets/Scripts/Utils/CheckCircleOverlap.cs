using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew
{
    public class CheckCircleOverlap : MonoBehaviour
    {
        [SerializeField] private float _radius = 1f;
        [SerializeField] private LayerMask _mask;
        [SerializeField] private string[] _tags;     

        [SerializeField] private OnOverlapEvent _onOverlap;
        private Collider2D[] interactionResult = new Collider2D[10];
        
        private void OnDrawGizmosSelected()
        {
            Handles.DrawSolidDisc(transform.position,Vector3.forward, _radius);
            Handles.color = HandlesUtils.TransparentRed;
        }

        public void Check()
        {
            var size = Physics2D.OverlapCircleNonAlloc(transform.position, _radius, interactionResult,_mask);
            
            var overLaps = new List<GameObject>();
            for (int i = 0; i < size; i++)
            {
                var overLapResult = interactionResult[i];
                
                var isInTags = _tags.Any(tag => interactionResult[i].CompareTag(tag));
                if (isInTags)
                {
                     _onOverlap?.Invoke(interactionResult[i].gameObject);
                }
            }
            
            
        }

        [Serializable]
        public class OnOverlapEvent : UnityEvent<GameObject>
        {
            
        }
    }
}

