using EventBusSystem;
using UnityEngine;

[CreateAssetMenu(menuName = "Configurations/Quest/QuestDestroyCar", fileName = "QuestDestroyCar", order = 0)]
public class QuestDestroyCar : TaskQuest, IDestroyCarDetach
{
    [SerializeField] private float neededDestroyCar;
    [SerializeField] private string processDescription;

    private float amountDestroyCar;
    private AttachCarQueueController attachCarQueueController;

    public override float ProcessDescription()
    {
        return amountDestroyCar / neededDestroyCar;
    }

    public override void EnableTask()
    {
        EventBus.Subscribe(this);
        amountDestroyCar = 0;
    }

    public override void DisableTask()
    {
        EventBus.Unsubscribe(this);
    }

    public override bool IsTaskCompleted()
    {
        return amountDestroyCar >= neededDestroyCar;
    }

    public void CarDetach()
    {
        amountDestroyCar++;
        ProcessUpdated?.Invoke();
    }
}