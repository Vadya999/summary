using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Inventory_Save_System
{
    public class SaveSystemInventory
    {
        public static void SaveInventory(List<ItemScriptableObject> listSO, List<int> listIntCountItems)
        {
            InventoryData inventoryData = new InventoryData(listSO, listIntCountItems);
            string path = Application.persistentDataPath + "/InventoryData.dat";

            var json = JsonUtility.ToJson(inventoryData);
            File.WriteAllText(path, json);
        }

        public static InventoryData LoadInventoryData()
        {
            string path = Application.persistentDataPath + "/InventoryData.dat";

            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                if (string.IsNullOrWhiteSpace(json)) return new InventoryData();
                InventoryData inventoryData = JsonUtility.FromJson<InventoryData>(json);

                return inventoryData;
            }

            else
            {
                return null;
            }
        }
    }
}