using System.Collections;
using DG.Tweening;
using Kuhpik;
using MoreMountains.NiceVibrations;
using Source.Scripts.Components;
using Source.Scripts.Signals;
using UnityEngine;

namespace Source.Scripts.Systems
{
    public class ScannerSystem : GameSystem
    {
        public override void OnInit()
        {
            game.playerComponent.ScannerListener.TriggerEnterEvent += TryScanning;
            game.playerComponent.ScannerListener.TriggetExitEvent += Exit;
        }

        private void TryScanning(Transform other)
        {
            var scanner = other.GetComponent<ScannerComponent>();

            if (scanner && game.currentDragItem)
            {
                if (game.currentDragItem is ItemComponent item)
                {
                    if (item.Id == game.neededItemId && !item.IsScanned)
                    {
                        scanner.InZone(true);
                        game.canMove = false;
                        scanner.Scanning();
                        StartCoroutine(ScanRoutine(item));
                    }
                }
            }
        }
        
        private void Exit(Transform other)
        {
            var scanner = other.GetComponent<ScannerComponent>();

            if (scanner) scanner.InZone(false);
        }

        private IEnumerator ScanRoutine(ItemComponent item)
        {
            yield return new WaitForSeconds(2f);
            
            game.canMove = true;
            item.Scanning();
            Supyrb.Signals.Get<ScannerSignal>().Dispatch();
            game.currentDragItem.transform.DOPunchScale(new Vector3(0.3f, 0.3f, 0.3f), 0.3f, 1);
        }
    }
}