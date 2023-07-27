using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Kuhpik;
using MoreMountains.NiceVibrations;
using UnityEngine;

namespace Source.Scripts.Components
{
    public class ShowcaseComponent : MonoBehaviour
    {
        [SerializeField] private ItemInfo[] itemInfos;
        [SerializeField] private Animator animator;
        [SerializeField] private ZoneUIComponent zone;
        
        private Dictionary<string, ItemComponent> items = new Dictionary<string, ItemComponent>();

        public event Action OnItemGet;

        private void Start()
        {
            SpawnItems();
        }

        private void SpawnItems()
        {
            foreach (var itemInfo in itemInfos)
            {
                var item = Instantiate(itemInfo.ItemConfig.Prefab, itemInfo.SpawnPosition.position, itemInfo.SpawnPosition.rotation, transform);
                item.transform.localScale = Vector3.zero;
                item.transform.DOScale(Vector3.one, 0.5f);
                item.SetItemId(itemInfo.ItemConfig.Id);
                items.Add(item.Id, item);
            }
        }

        public bool CanGet(string id)
        {
            return items.ContainsKey(id);
        }
        
        public void SpawnItem(string id)
        {
            animator.SetBool("Open", false);
            var info = itemInfos.First(x => x.ItemConfig.Id == id);
            
            var item = Instantiate(info.ItemConfig.Prefab, info.SpawnPosition.position, info.SpawnPosition.rotation, transform);
            item.transform.localScale = Vector3.zero;
            item.transform.DOScale(Vector3.one, 0.5f);
            item.SetItemId(info.ItemConfig.Id);
            items[id] = item;
        }
        
        public ItemComponent GetItem(string id)
        {
            MMVibrationManager.Haptic(HapticTypes.Selection);
            OnItemGet?.Invoke();
            animator.SetBool("Open", true);
            return items[id];
        }

        public void InZone(bool value)
        {
            zone.InZone(value);
        }

        [Serializable]
        public class ItemInfo
        {
            [field:SerializeField] public ItemConfig ItemConfig { get; private set; }
            [field:SerializeField] public Transform SpawnPosition { get; private set; }
        }
    }
}