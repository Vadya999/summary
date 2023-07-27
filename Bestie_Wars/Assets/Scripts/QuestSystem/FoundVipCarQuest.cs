using EventBusSystem;
using UnityEngine;

[CreateAssetMenu(menuName = "Configurations/Quest/FoundVipCarQuest", fileName = "FoundVipCarQuest", order = 0)]
public class FoundVipCarQuest : TaskQuest, IVipeCarDetach
{
    [SerializeField] private float neededVipCar;
    [SerializeField] private string descriptionProcess;
    private float amountCar;
    private AttachCarQueueController attachCarQueueController;

    public override float ProcessDescription()
    {
        return amountCar / neededVipCar;
    }

    public override void EnableTask()
    {
        amountCar = 0;
        EventBus.Subscribe(this);
    }

    public override void DisableTask()
    {
        EventBus.Unsubscribe(this);
    }

    public override bool IsTaskCompleted()
    {
        return amountCar >= neededVipCar;
    }

    public void CarDetach()
    {
        amountCar++;
        ProcessUpdated?.Invoke();
    }
}