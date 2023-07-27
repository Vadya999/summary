using System;
using UnityEngine;

namespace Tutorial.Steps
{
    [Serializable]
    public class WaitForItemAppear : TutorialStep
    {
        [SerializeField] private ItemType _itemType;

        public override void Update()
        {
            if (gameData.activeRoom != null && HasItem())
            {
                Complete();
            }
        }

        private bool HasItem()
        {
            for (int i = 0; i < gameData.activeRoom.items.Length; i++)
            {
                if (gameData.activeRoom.items[i].itemType == _itemType)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
