using System;
using System.Collections;
using UnityEngine;
using StateMachine = StateMachine.StateMachine;

[RequireComponent(typeof(Collider))]
public abstract class PlayerTriggerZone : MonoBehaviour
{
    public bool IsPlayerInZone;

    public event Action PlayerEnteredZone;
    public event Action PlayerExitedZone;

    private void Awake()
    {
        GetComponent<Collider>().isTrigger = true;
        AwakeFake();
    }

    protected virtual void AwakeFake()
    {
    }

    protected virtual void PlayerEnterZone()
    {
    }

    protected virtual void PlayerExitZone()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerTriggerComponent>())
        {
            Debug.Log("Enter");
            PlayerEnterZone();
            IsPlayerInZone = true;
            PlayerEnteredZone?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerTriggerComponent>())
        {
            Debug.Log("Exit");
            PlayerExitZone();
            IsPlayerInZone = false;
            PlayerExitedZone?.Invoke();
        }
    }
}