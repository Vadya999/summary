using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using FactoryPool;
using Kuhpik;
using TMPro;
using UnityEngine;

public class UIMoney : MonoPooled
{
    [SerializeField] private TMP_Text Text;
    [SerializeField] private Transform objectT;

    public void Show(int addMoney)
    {
        objectT.transform.localPosition = new Vector3(0,0.9f,0);
        Text.text = $"+{addMoney}";
        objectT.DOMove(objectT.transform.position + new Vector3(0, 3, 0), 1.7f).OnComplete(ReturnToPool);
    }
}