using EventBusSystem;
using UnityEngine;

[CreateAssetMenu(menuName = "Configurations/Quest/CarTowing", fileName = "CarTowing", order = 0)]
public class CarTowing : TaskQuest, IFreeCarDetach
{
    [SerializeField] private string descriptionText = "towing car";
    [SerializeField] private float amountNeededCar;

    private float amountCar;

    public override float ProcessDescription()
    {
        return amountCar / amountNeededCar;
    }

    public override void EnableTask()
    {
        EventBus.Subscribe(this);
        amountCar = 0;
    }

    public override void DisableTask()
    {
        amountCar = 0;
        EventBus.Unsubscribe(this);
    }

    public void CarDetach()
    {
        Debug.Log("Detach");
        amountCar++;
        ProcessUpdated?.Invoke();
    }

    public override bool IsTaskCompleted()
    {
        return amountCar >= amountNeededCar;
    }
}