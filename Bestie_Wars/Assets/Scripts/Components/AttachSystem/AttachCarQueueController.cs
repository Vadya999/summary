using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using EventBusSystem;
using Kuhpik;
using TMPro;
using UnityEngine;

public class AttachCarQueueController : MonoBehaviour, IQueue
{
    [SerializeField] private Transform car;
    [SerializeField] private CharacterCarMovementSystem characterCarMovementSystem;
    [SerializeField] private GameObject textImage;
    [SerializeField] private TMP_Text amountText;
    [SerializeField] private Transform firstObjectPosition;

    private List<IQueuing> attachedObject = new List<IQueuing>();
    private int maxAmout;

    public event Action DetachCarWithoutDestroyAndLegacy;
    public event Action DetachCarDestroy;
    public event Action AttachedVipCar;

    public bool IsCanBeAttach => attachedObject.Count < maxAmout;
    public bool IsCanBeDetach => attachedObject.Count != 0;

    public void Initialize()
    {
        Bootstrap.Instance.StateEnterEvent += ChangeState;
    }

    private void DisableText()
    {
        textImage.SetActive(false);
        amountText.gameObject.SetActive(false);
    }

    private void EnableText()
    {
        textImage.SetActive(true);
        amountText.gameObject.SetActive(true);
    }

    private void ChangeState(GameStateID gameStateID)
    {
        if (gameStateID == GameStateID.Game) EnableText();
        else DisableText();
    }

    public void Attach(IQueuing attachObject)
    {
        if (attachedObject.Contains(attachObject) || IsCanBeAttach == false)
        {
            Debug.Log("Error, you cannot attach an already attached vehicle or attachedObject > maxAmount");
            return;
        }

        var carNew = attachObject.TransformObject.GetComponent<AttachCarController>();
        if (carNew && carNew.IsCanBeVip)
        {
            EventBus.RaiseEvent<IVipeCarDetach>(t => t.CarDetach());
        }

        var pos = attachedObject.Count == 0 ? firstObjectPosition : attachedObject.Last().PositionBehind;
        attachObject.TransformObject.DOJump(pos.position, 1, 1, 0.5f);
        attachObject.TransformObject.DORotateQuaternion(pos.rotation, 0.5f);
        attachObject.TransformObject.parent = car;
        attachObject.TransformObject.localScale = new Vector3(1, 1, -1);
        attachedObject.Add(attachObject);
        attachObject.Attach(pos);
    }

    public void AttachWithoutJump(IQueuing attachObject)
    {
        if (attachedObject.Contains(attachObject) || IsCanBeAttach == false)
        {
            Debug.Log("Error, you cannot attach an already attached vehicle or attachedObject > maxAmount");
            return;
        }

        var pos = attachedObject.Count == 0 ? firstObjectPosition : attachedObject.Last().PositionBehind;
        var sequcene = DOTween.Sequence();
        sequcene.Append(attachObject.TransformObject.DOScale(Vector3.zero, 0.3f));
        sequcene.Append(DOVirtual.DelayedCall(0, () =>
        {
            attachObject.TransformObject.position = pos.position;
            attachObject.TransformObject.rotation = pos.rotation;
            attachObject.TransformObject.localScale = new Vector3(1, 1, -1);
            attachObject.TransformObject.parent = car;
        }));
        attachedObject.Add(attachObject);
        attachObject.Attach(pos);
    }

    public void DetachAll()
    {
        foreach (var attachCar in attachedObject)
        {
            Detach(attachCar);
            attachCar.TransformObject.GetComponent<AttachCarController>().AttachPause(2f);
            DetachAll();
            break;
        }
    }

    public void SetMaxLevel(int amount)
    {
        maxAmout = amount;
    }

    public void AttachToBehindWithoutRotation(IQueuing attachObject)
    {
        if (attachedObject.Contains(attachObject) || IsCanBeAttach == false)
        {
            Debug.Log("Error, you cannot attach an already attached vehicle or attachedObject > maxAmount");
            return;
        }

        var pos = attachedObject.Count == 0 ? firstObjectPosition : attachedObject.Last().PositionBehind;
        attachObject.TransformObject.position = pos.position;
        attachObject.TransformObject.rotation = pos.rotation;
        attachObject.TransformObject.localScale = new Vector3(1, 1, -1);
        attachedObject.Add(attachObject);
        attachObject.Attach(pos);
    }

    private void ReShake()
    {
        foreach (var attached in attachedObject)
        {
            attached.TransformObject.DOShakeScale(0.3f, 0.6f);
        }
    }

    private void Update()
    {
        amountText.text = $"{attachedObject.Count}/{maxAmout}";
    }

    public void Detach(IQueuing detachObject)
    {
        if (attachedObject.Contains(detachObject) == false)
        {
            Debug.Log("Error, detach object not found");
            return;
        }

        detachObject.Detach();
        attachedObject.Remove(detachObject);
        detachObject.TransformObject.parent = null;
    }

    public void Recalculate()
    {
        var allIQueuing = new List<IQueuing>();
        foreach (var queuing in attachedObject)
        {
            allIQueuing.Add(queuing);
            queuing.Detach();
        }

        attachedObject = new List<IQueuing>();
        foreach (var queuing in allIQueuing)
        {
            queuing.AttachToNewQueue(this, false);
        }

        ReShake();
    }

    public IQueuing DetachLast()
    {
        var last = attachedObject.Last();
        Detach(last);
        return last;
    }

    public bool IsCanBeDetachDestoryCar => attachedObject.Where(t =>
        t.TransformObject.GetComponent<AttachCarController>().IsCanBeDestroy).ToList().Count != 0;

    public IQueuing GetCarWithDestroy(bool isHeavy)
    {
        var carCanBeDestroy = attachedObject
            .Where(t => CheckDestroyCar(t, isHeavy)).ToList();
        if (carCanBeDestroy.Count == 0) return null;
        var last = carCanBeDestroy.Last();
        Detach(last);
        EventBus.RaiseEvent<IDestroyCarDetach>(t => t.CarDetach());
        return last;
    }

    public bool IsCanBeDetachLegacyCar => attachedObject.Where(t =>
        t.TransformObject.GetComponent<AttachCarController>().IsCanBeSave).ToList().Count != 0;

    private bool CheckDestroyCar(IQueuing queuing, bool isHeavyPress)
    {
        var attachCar = queuing.TransformObject.GetComponent<AttachCarController>();
        var isDestroy = attachCar.IsCanBeDestroy;
        var isHeavy = attachCar.IsHeavyCar;
        if (isHeavy)
        {
            return isHeavyPress && isDestroy;
        }

        return isDestroy;
    }

    public IQueuing GetCarLegacy()
    {
        var carCanBeDestroy = attachedObject.Where(t =>
            t.TransformObject.GetComponent<AttachCarController>().IsCanBeSave).ToList();
        if (carCanBeDestroy.Count == 0) return null;
        var last = carCanBeDestroy.Last();
        Detach(last);
        return last;
    }

    public bool IsCanBeDetachCar => attachedObject.Where(t =>
    {
        var attach = t.TransformObject.GetComponent<AttachCarController>();
        return attach.IsCanBeSave == false && attach.IsCanBeDestroy == false;
    }).Count() != 0;

    public IQueuing GetCarWithoutDestroyAndLegacy()
    {
        EventBus.RaiseEvent<IFreeCarDetach>(t => t.CarDetach());
        var carCanBeDestroy = attachedObject.Where(t =>
        {
            var attach = t.TransformObject.GetComponent<AttachCarController>();
            return attach.IsCanBeDestroy == false && attach.IsCanBeSave == false;
        }).ToList();
        if (carCanBeDestroy.Count == 0) return null;
        var last = carCanBeDestroy.Last();
        Detach(last);
        return last;
    }
}