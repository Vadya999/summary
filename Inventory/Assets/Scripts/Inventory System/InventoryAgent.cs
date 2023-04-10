using System;
using System.Collections;
using System.Collections.Generic;
using Inventory_Save_System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class InventoryAgent : MonoBehaviour
{
    [SerializeField] private Transform inventorySlotTransform;
    [SerializeField] private List<InventorySlot> slots = new List<InventorySlot>(30);
    [SerializeField] private ItemScriptableObject[] itemScriptableObjects;

    [Space] [Header("Inventory Buttons")] [SerializeField]
    private Button shotButton;

    [SerializeField] private Button addCartridgesButton;
    [SerializeField] private Button addRandomItemButton;
    [SerializeField] private Button cleanRandomSlotButton;

    private int openedSlots = 15;
    private int existSlots = 8;

    private void Start()
    {
        AddInventorySlotsToList();

        SubscribeButtons();

        LoadSaveInventoryData();
    }

    public void Shot()
    {
        bool oneCount = false;

        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].myItemScriptableObject != null &&
                slots[i].myItemScriptableObject.itemType == ItemsType.Consumables && !oneCount)
            {
                slots[i].RemoveItem(1);
                oneCount = true;
            }
        }

        if (!oneCount)
        {
            Debug.Log("No Cartridges");
        }
    }

    public void AddCartridges()
    {
        bool oneCount = false;

        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].myItemScriptableObject != null &&
                slots[i].myItemScriptableObject.itemType == ItemsType.Consumables)
            {
                slots[i].AddItem(slots[i].myItemScriptableObject, slots[i].myItemScriptableObject.maxAmount);
                oneCount = true;
            }
        }

        if (!oneCount)
        {
            Debug.Log("Cartridges Slots Empty");
        }
    }

    public void AddItemRandomItem()
    {
        var randomItem = itemScriptableObjects[Random.Range(0, itemScriptableObjects.Length)];
        bool isAddedItem = false;

        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].myItemScriptableObject == randomItem)
            {
                slots[i].AddItem(randomItem, 1);
                isAddedItem = true;
                break;
            }
        }

        if (!isAddedItem)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (slots[i].isEmptySlot)
                {
                    slots[i].AddItem(randomItem, 1);
                    return;
                }
            }
        }
    }

    public void CleanRandomSlot()
    {
        List<int> filled = new List<int>(openedSlots);

        for (int i = 0; i < slots.Count; i++)
        {
            if (!slots[i].isEmptySlot)
            {
                filled.Add(i);
            }
        }

        if (filled.Count > 0)
        {
            int index = Random.Range(0, filled.Count);
            int itemCount = slots[filled[index]].countItn;
            slots[filled[index]].RemoveItem(itemCount);
            filled.Clear();
        }
        else
        {
            Debug.Log("All Slots Empty");
        }
    }

    private void AddInventorySlotsToList()
    {
        for (int i = 0; i < inventorySlotTransform.childCount; i++)
        {
            var inventorySlot = inventorySlotTransform.GetChild(i).GetComponent<InventorySlot>();
            slots.Add(inventorySlot);
        }
    }

    private void LoadSaveInventoryData()
    {
        var inventoryData = SaveSystemInventory.LoadInventoryData();

        for (int i = 0; i < existSlots; i++)
        {
            if (i < inventoryData.itemScriptableObjects.Count && i < inventoryData.itemsCount.Count && inventoryData.itemScriptableObjects[i] != null)
            {
                slots[i].myItemScriptableObject = inventoryData.itemScriptableObjects[i];
                slots[i].countItn = inventoryData.itemsCount[i];
                slots[i].slotImage.sprite = inventoryData.itemScriptableObjects[i].itemImage;
                slots[i].isEmptySlot = false;
            }
        }
    }

    private void SubscribeButtons()
    {
        shotButton.onClick.AddListener(Shot);
        addCartridgesButton.onClick.AddListener(AddCartridges);
        addRandomItemButton.onClick.AddListener(AddItemRandomItem);
        cleanRandomSlotButton.onClick.AddListener(CleanRandomSlot);
    }

    private void UnSubscribeButtons()
    {
        shotButton.onClick.RemoveListener(Shot);
        addCartridgesButton.onClick.RemoveListener(AddCartridges);
        addRandomItemButton.onClick.RemoveListener(AddItemRandomItem);
        cleanRandomSlotButton.onClick.RemoveListener(CleanRandomSlot);
    }

    private void OnApplicationQuit()
    {
        SaveInventoryData();
    }

    private void SaveInventoryData()
    {
        List<int> itemsCount = new List<int>(15);
        List<ItemScriptableObject> itemScriptableObjects = new List<ItemScriptableObject>(15);
        
        for (int i = 0; i < existSlots; i++)
        {
            if (slots[i].myItemScriptableObject != null)
            {
                itemsCount.Add(slots[i].countItn); 
                itemScriptableObjects.Add(slots[i].myItemScriptableObject); 
            }
        }
        
        SaveSystemInventory.SaveInventory(itemScriptableObjects, itemsCount);
    }

    private void OnDestroy()
    {
        UnSubscribeButtons();
    }
}