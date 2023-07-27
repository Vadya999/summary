using DG.Tweening;
using EventBusSystem;
using Kuhpik;
using StateMachine;
using UnityEngine;

public class AttachStartState : State
{
    private readonly IQueuing queuing;
    private readonly AttachCarController m_attachCarController;
    private readonly CharacterCarMovementSystem carMovementSystem;

    private IQueue controller;
    public bool IsReadyToLeave;

    public AttachStartState(IQueuing queuing, AttachCarController attachCarController)
    {
        this.queuing = queuing;
        carMovementSystem = Bootstrap.Instance.GetSystem<CharacterCarMovementSystem>();
        m_attachCarController = attachCarController;
    }

    public void SetAttachCarQueueController(IQueue attachCarQueueController)
    {
        controller = attachCarQueueController;
    }

    public override void OnStateEnter()
    {
        if (controller != null)
        {
            carMovementSystem.Rigidbody.isKinematic = true;
            carMovementSystem.AddModification(PlayerSpeedModification.Attach, 0f);
            // carMovementSystem.Rigidbody.isKinematic = true;
            // carMovementSystem.AddModification(PlayerSpeedModification.Attach, 0);
            // var transformAttach = carMovementSystem.Cran;
            // var position = carMovementSystem.CranAttachPosition;
            // var startRotate = transformAttach.rotation;
            var sequence = DOTween.Sequence();
            // sequence.Append(transformAttach.DOLookAt(
            //     new Vector3(queuing.TransformObject.position.x, transformAttach.position.y,
            //         queuing.TransformObject.position.z), 1.2f));
            sequence.Append(DOVirtual.DelayedCall(0.5f,
                () =>
                {
                    carMovementSystem.AddModification(PlayerSpeedModification.Attach, 1f);
                    carMovementSystem.Rigidbody.isKinematic = false;
                }));
            // sequence.Append(transformAttach.DORotateQuaternion(startRotate, 1.2f));
            // sequence.OnComplete(() =>
            // {
            // carMovementSystem.AddModification(PlayerSpeedModification.Attach, 1);
            controller.Attach(queuing);
            IsReadyToLeave = true;
            //   carMovementSystem.Rigidbody.isKinematic = false;
            // });
        }
        else
        {
            IsReadyToLeave = true;
            Debug.Log("Attach skipped, controller==null");
        }
    }

    public override void OnStateExit()
    {
        controller = null;
        IsReadyToLeave = false;
    }
}

public class AttachStartStateWithoutAnimation : State
{
    private readonly IQueuing queuing;

    private IQueue controller;
    public bool IsReadyToLeave;

    public AttachStartStateWithoutAnimation(IQueuing queuing)
    {
        this.queuing = queuing;
    }

    public void SetAttachCarQueueController(IQueue attachCarQueueController)
    {
        controller = attachCarQueueController;
    }

    public override void OnStateEnter()
    {
        if (controller != null)
        {
            var seuence = DOTween.Sequence();
            var currentTransform = queuing.TransformObject;
            currentTransform.transform.localScale = new Vector3(1,1,-1);
            seuence.OnComplete(() =>
            {
                if (controller.IsCanBeAttach)
                {
                    controller.AttachToBehindWithoutRotation(queuing);
                }

                currentTransform.transform.DOShakeScale(0.4f).OnComplete(() =>
                {
                    currentTransform.transform.localScale = new Vector3(1,1,-1);
                    IsReadyToLeave = true;
                });
            });
        }
        else
        {
            IsReadyToLeave = true;
            Debug.Log("Attach skipped, controller==null");
        }
    }

    public override void OnStateExit()
    {
        controller = null;
        IsReadyToLeave = false;
    }
}