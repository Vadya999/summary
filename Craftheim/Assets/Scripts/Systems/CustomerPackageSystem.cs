using System.Linq;
using DG.Tweening;
using Kuhpik;
using Source.Scripts.Components;
using Source.Scripts.Signals;
using UnityEngine;

namespace Source.Scripts.Systems
{
    public class CustomerPackageSystem : GameSystem
    {
        [SerializeField] private float animDuration = 0.5f;

        private ItemComponent currentPackageItem;
        
        public override void OnInit()
        {
            Supyrb.Signals.Get<ReplaceCustomerPackageSignal>().AddListener(ReplacePackage);
            game.playerComponent.CollisionListener.TriggerEnterEvent += TryMoveToPackage;
            game.playerComponent.CollisionListener.TriggetExitEvent += Exit;
        }

        private void Exit(Transform other)
        {
            var customerPackageComponent = other.GetComponent<CustomerPackageComponent>();

            if (customerPackageComponent)
            {
                customerPackageComponent.InZone(false);
            }
        }

        private void TryMoveToPackage(Transform other)
        {
            var customerPackageComponent = other.GetComponent<CustomerPackageComponent>();

            if (customerPackageComponent && game.currentDragItem)
            {
                if (game.currentDragItem is ItemComponent item)
                {
                    if (!item.IsScanned) return;

                    customerPackageComponent.InZone(true);
                    currentPackageItem = item;
                    game.needTakeMoney = true;
                    game.playerComponent.PlayerDragItemComponent.RemoveItemToDrag();
                    game.customerComponent.PayMoney(() => game.moneyZoneComponent.CreateMoney(game.items.First(x => x.Id == item.Id)));
                    currentPackageItem.transform.DOJump(customerPackageComponent.PackagePos.position + new Vector3(Random.Range(-1.5f, 1.5f), 0, Random.Range(-1.5f, 1.5f)), 7f, 1, animDuration)
                        .OnComplete(() =>
                        {
                            customerPackageComponent.MovedToPackage();
                            currentPackageItem.Coll.enabled = true;
                            currentPackageItem.gameObject.AddComponent<Rigidbody>();
                        });
                }
            }
        }

        private void ReplacePackage()
        {
            var packageStartPos = game.customerPackageComponent.Package.position;
            var startPackagePosY = packageStartPos.y;
            var startPackagePosX = packageStartPos.x;
            var anim = DOTween.Sequence();
            
            Destroy(currentPackageItem.GetComponent<Rigidbody>());
            currentPackageItem.transform.SetParent(game.customerPackageComponent.Package);

            anim.Append(game.customerPackageComponent.Package.transform.DOMoveY(-10, .5f));
            anim.Append(game.customerPackageComponent.Package.transform.DOMoveX(-25, .5f));
            anim.AppendCallback(() =>
            {
                Supyrb.Signals.Get<NewCustomerSignal>().Dispatch(false);
                Destroy(currentPackageItem.gameObject);
            });
            anim.Append(game.customerPackageComponent.Package.transform.DOMoveX(startPackagePosX, .5f));
            anim.Append(game.customerPackageComponent.Package.transform.DOMoveY(startPackagePosY, .5f));
        }
    }
}