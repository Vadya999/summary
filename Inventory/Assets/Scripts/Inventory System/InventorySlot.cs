using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [HideInInspector] public int countItn;
    [HideInInspector] public ItemScriptableObject myItemScriptableObject;

    [Header("General Settings")] 
    public bool isUnlock;
    public Image slotImage;
    [SerializeField] private TextMeshProUGUI count;
    [SerializeField] private Button slotButton;
    [SerializeField] private Sprite lockImageSlotIcon;
    [SerializeField] private TextMeshProUGUI priceToUnlockTMP;
    [SerializeField] private int priceToUnlock;


    [Space] [Header("View Settings Item")] [SerializeField]
    private GameObject viewGameObjectItem;
    [SerializeField] private Image viewImageItem;
    [SerializeField] private Button exitButtonViewItem;
    
    public bool isEmptySlot { get; set; }
    private int maxCount;

    private void Start()
    {
        isEmptySlot = true;
        slotButton.onClick.AddListener(ViewSlot);
        count.text = countItn.ToString();
        CheckOneCountToHide();
        CheckLockState();
    }

    public void AddItem(ItemScriptableObject itemScriptableObject, int addCount)
    {
        for (int i = 0; i < addCount; i++)
        {
            maxCount = itemScriptableObject.maxAmount;
            myItemScriptableObject = itemScriptableObject;
            slotImage.sprite = itemScriptableObject.itemImage;
            countItn++;
            if (isCrowded()) return;
            count.text = countItn.ToString();
            isEmptySlot = false;

            CheckOneCountToHide();
        }
    }

    public void RemoveItem(int removeCount)
    {
        for (int i = 0; i < removeCount; i++)
        {
            countItn--;
            count.text = count.ToString();
            if (countItn == 0)
            {
                count.text = "0";
                slotImage.sprite = null;
                isEmptySlot = true;
                myItemScriptableObject = null;
            }

            CheckOneCountToHide();
        }
    }

    private void ViewSlot()
    {
        if (myItemScriptableObject == null) return;

        viewGameObjectItem.SetActive(true);
        viewImageItem.sprite = myItemScriptableObject.itemImage;
        exitButtonViewItem.onClick.AddListener(ExitButtonView);
        
    }

    private void ExitButtonView()
    {
        viewGameObjectItem.SetActive(false);
    }

    private void CheckOneCountToHide()
    {
        if (countItn == 1)
        {
            count.text = "";
        }

        else
        {
            count.text = countItn.ToString();
        }
    }
    
    private bool isCrowded()
    {
        if (maxCount == countItn)
        {
            countItn--;
            count.text = countItn.ToString();
            return true;
        }

        return false;
    }

    private void CheckLockState()
    {
        if (isUnlock)
        {
            slotImage.sprite = null;
        }
        else
        {
            slotImage.sprite = lockImageSlotIcon;
            priceToUnlockTMP.text = priceToUnlock.ToString();
        }
    }

    private void OnDestroy()
    {
        slotButton.onClick.RemoveListener(ViewSlot);
    }
}