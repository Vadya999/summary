using System;
using System.Collections.Generic;

namespace Inventory_Save_System
{
    [Serializable]
    public class InventoryData
    {
        public List<int> itemsCount = new List<int>(countItems);
        public List<ItemScriptableObject> itemScriptableObjects = new List<ItemScriptableObject>(countItems);

        private static int countItems = 10;

        public InventoryData(List<ItemScriptableObject> listSOItems, List<int> listIntCountItems)
        {
            itemsCount = listIntCountItems;
            itemScriptableObjects = listSOItems;
        }

        public InventoryData()
        {
        }
    }
}