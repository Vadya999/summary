using System;
using DG.Tweening;
using StateMachine;
using UnityEngine;

public class CarZoneWaitingState : State
{
    private readonly TimerConfiguration timerConfiguration;
    private readonly Transform zone;
    private AttachCarController attachCarController;

    public bool IsReadyToLeave;

    public CarZoneWaitingState(TimerConfiguration timerConfiguration, Transform zone)
    {
        this.timerConfiguration = timerConfiguration;
        this.zone = zone;
        timerConfiguration.Image.gameObject.SetActive(false);
        timerConfiguration.Image2.gameObject.SetActive(false);
        timerConfiguration.Text.gameObject.SetActive(false);
    }

    public void SetAttach(AttachCarController attachCarController)
    {
        this.attachCarController = attachCarController;
    }

    public override void OnStateEnter()
    {
        var sequence = DOTween.Sequence();
        var scale = attachCarController.transform.localScale;
        sequence.Append(attachCarController.transform.DOScale(Vector3.zero, 0.3f));
        sequence.Append(DOVirtual.DelayedCall(0, () =>
        {
            attachCarController.transform.position = zone.position;
            attachCarController.transform.rotation = zone.rotation;
            attachCarController.transform.localScale = scale;
        }));
        sequence.Append(attachCarController.transform.DOShakeScale(0.4f, 0.7f)).OnComplete(() =>
        {
            timerConfiguration.Image.gameObject.SetActive(true);
            timerConfiguration.Image2.gameObject.SetActive(true);
            timerConfiguration.Image.fillAmount = 0;
            timerConfiguration.Text.gameObject.SetActive(true);
            timerConfiguration.Text.text = attachCarController.AttachCarConfigurations.WaitingSellTime.ToString();
            timerConfiguration.Image.DOFillAmount(1, attachCarController.AttachCarConfigurations.WaitingSellTime)
                .OnComplete(() => IsReadyToLeave = true);
        });
    }

    public override void Tick()
    {
        // timerConfiguration.Text.text = Convert.ToInt32(attachCarController.AttachCarConfigurations.WaitingSellTime -
        //                                 ((1 - timerConfiguration.Image.fillAmount) *
        //                                  attachCarController.AttachCarConfigurations.WaitingSellTime)).ToString();
    }

    public override void OnStateExit()
    {
        timerConfiguration.Image.gameObject.SetActive(false);
        timerConfiguration.Image2.gameObject.SetActive(false);
        IsReadyToLeave = false;
        timerConfiguration.Text.gameObject.SetActive(false);
    }
}