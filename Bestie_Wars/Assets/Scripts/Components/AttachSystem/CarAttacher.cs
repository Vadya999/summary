using System;
using UnityEngine;

public class CarAttacher : MonoBehaviour
{
    [SerializeField] private AttachCarQueueController attachCarQueueController;
    [SerializeField] private TriggerListener triggerListener;

    private AttachCarController currentCarController;

    private void OnEnable()
    {
        triggerListener.OnTriggerEnterEvent += TriggerEnter;
    }

    private void OnDisable()
    {
        triggerListener.OnTriggerEnterEvent -= TriggerEnter;
    }

    private void Update()
    {
        if (currentCarController != null)
        {
            if (currentCarController.IsAttach)
            {
                currentCarController = null;
            }
        }
    }

    private void TriggerEnter(Transform other)
    {
        if (attachCarQueueController.IsCanBeAttach == false || currentCarController != null)
        {
            return;
        }

        var queuing = other.GetComponent<AttachCarController>();
        if (queuing != null && queuing.IsCanBeAttach && queuing.isActiveAndEnabled)
        {
            currentCarController = queuing;
            currentCarController.ShowAttach(attachCarQueueController);
        }
        else
        {
            queuing.FailedAttach();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var queuing = other.GetComponent<AttachCarController>();
        if (queuing != null && queuing.IsCanBeAttach)
        {
            if (currentCarController != null && queuing == currentCarController)
            {
                currentCarController.DisableAttach();
                currentCarController = null;
            }
        }
    }
}