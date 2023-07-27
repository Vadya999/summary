using System;
using System.Collections;
using System.Collections.Generic;
using EventBusSystem;
using FactoryPool;
using Kuhpik;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Money : MonoPooled
{
    private int currentAdd;
    private Rigidbody rb;

    public Rigidbody Rigidbody => rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerTriggerComponent>())
        {
            TakeMoney();
        }
    }

    private void TakeMoney()
    {
        Bootstrap.Instance.GetSystem<MoneySpawnUISystem>().SpawnMoney(currentAdd, transform);
        Bootstrap.Instance.PlayerData.Money += currentAdd;
        EventBus.RaiseEvent<IUpdateMoney>(t => t.UpdateMoney());
        ReturnToPool(); VibrationSystem.PlayVibration();
        Bootstrap.Instance.SaveGame();
    }

    public void SetMoney(int add)
    {
        currentAdd = add;
    }
}