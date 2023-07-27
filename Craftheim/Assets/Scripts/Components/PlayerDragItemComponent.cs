using System;
using DG.Tweening;
using Kuhpik;
using NaughtyAttributes;
using UnityEngine;

namespace Source.Scripts.Components
{
    public class PlayerDragItemComponent : MonoBehaviour
    {
        public event Action<bool> OnChanged;
        
        private bool isFull;

        public void AddItemToDrag(DragComponent item, Action onComplete = null)
        {
            if (Bootstrap.Instance.GameData.currentDragItem != null) return;

            isFull = true;
            OnChanged?.Invoke(isFull);
            
            Bootstrap.Instance.GameData.canMove = false;
            var playerTr =  Bootstrap.Instance.GameData.playerComponent.transform;
            var playerEulerAngles = playerTr.eulerAngles;
            playerTr.rotation = Quaternion.Euler(playerEulerAngles.x, - playerEulerAngles.y, playerEulerAngles.z);

            var tr = transform;
            var pos = tr.localPosition;
            var itemTransform = item.transform;
            
            tr.localPosition = new Vector3(pos.x, item.PosY, item.PosZ);
            tr.localRotation = Quaternion.Euler(item.Rotation.x, item.Rotation.y, item.Rotation.z);
            itemTransform.parent = null;

            item.transform.DORotate(tr.eulerAngles, 0.5f);
            item.transform.DOJump(tr.position,1.5f, 1, 0.5f).OnComplete(() =>
            {
                Bootstrap.Instance.GameData.currentDragItem = item;
                
                Bootstrap.Instance.GameData.canMove = true;
                onComplete?.Invoke();
            });
        }
        
        public void RemoveItemToDrag()
        {
            if (Bootstrap.Instance.GameData.currentDragItem == null) return;
            
            isFull = false;
            Bootstrap.Instance.GameData.currentDragItem = null;
            OnChanged?.Invoke(isFull);
        }
    }
}