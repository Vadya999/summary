using System.Collections.Generic;
using DG.Tweening;
using HomaGames.HomaBelly;
using Kuhpik;
using StateMachine;
using UnityEngine;

public class CarZoneDestroyState : State
{
    private readonly GameObject destroyImage;
    private AttachCarController attachCarController;

    public CarZoneDestroyState(GameObject destroyImage)
    {
        this.destroyImage = destroyImage;
    }

    public void SetAttachCarController(AttachCarController attachCarController)
    {
        this.attachCarController = attachCarController;
    }

    public override void OnStateEnter()
    {
        destroyImage.SetActive(true);
        attachCarController.ToDestroy();
        attachCarController.IsSelling = false;
        destroyImage.transform.DOShakeScale(0.5f, destroyImage.transform.localScale.x);
    }

    public override void OnStateExit()
    {
        destroyImage.SetActive(false);
    }
}

public class CarZoneLegacyState : State
{
    private readonly GameObject destroyImage;
    private AttachCarController attachCarController;

    public CarZoneLegacyState(GameObject destroyImage)
    {
        this.destroyImage = destroyImage;
    }

    public void SetAttachCarController(AttachCarController attachCarController)
    {
        this.attachCarController = attachCarController;
    }

    public override void OnStateEnter()
    {
        Bootstrap.Instance.PlayerData.amounLegacy++;
        destroyImage.SetActive(true);
        attachCarController.ToSave();
        attachCarController.IsSelling = false;
        destroyImage.transform.DOShakeScale(0.5f, destroyImage.transform.localScale.x);
    }

    public override void OnStateExit()
    {
        destroyImage.SetActive(false);
    }
}