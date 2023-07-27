using System;
using DG.Tweening;
using UnityEngine;

public class PodiumZone : MonoBehaviour
{
    [SerializeField] private Transform position;
    private AttachCarController currentAttachCar;

    public int AttachCarId => currentAttachCar.ID;
    public bool IsCanBeAttach => currentAttachCar == null;

    private void Update()
    {
        if (currentAttachCar != null && currentAttachCar.IsAttach)
        {
            currentAttachCar = null;
        }
    }

    public void Activate(AttachCarController currentAttachCar)
    {
        if (this.currentAttachCar == null)
        {
            currentAttachCar.AttachPause();
            this.currentAttachCar = currentAttachCar;
            var attachCar = currentAttachCar;
            if (attachCar != null)
            {
                var sequence = DOTween.Sequence();
                sequence.Append(attachCar.TransformObject.DOShakeScale(0.3f));
                sequence.Append(attachCar.TransformObject.DOScale(0, 0.4f));
                sequence.Append(attachCar.TransformObject.DOMove(position.position, 0.1f));
                sequence.Join(attachCar.TransformObject.DORotateQuaternion(position.rotation, 0.1f));
                sequence.Append(DOVirtual.DelayedCall(0,
                    () => { attachCar.TransformObject.localScale = Vector3.one; }));
                sequence.Append(attachCar.TransformObject.DOShakeScale(0.3f));
            }
        }
    }
}