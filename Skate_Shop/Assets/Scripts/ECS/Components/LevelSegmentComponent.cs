using UnityEngine;

[RequireComponent(typeof(NavigationBaker))]
public class LevelSegmentComponent : MonoBehaviour
{
    public MobsSpawnerComponent mobSpawner { get; private set; }
    public CashRegisterComponent cashRegister { get; private set; }
    public NavigationBaker navMesh { get; private set; }

    private void Awake()
    {
        cashRegister = GetComponentInChildren<CashRegisterComponent>();
        mobSpawner = GetComponentInChildren<MobsSpawnerComponent>();

        navMesh = GetComponent<NavigationBaker>();
    }
}