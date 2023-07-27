using System.Collections;
using System.Collections.Generic;
using EventBusSystem;
using Kuhpik;
using UnityEngine;

public class Cheat : MonoBehaviour
{
    public void AddMoney()
    {
        Bootstrap.Instance.PlayerData.Money += 1000;
        EventBus.RaiseEvent<IUpdateMoney>(t => t.UpdateMoney());
    }
}