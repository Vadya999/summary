using System;
using System.Collections;
using DG.Tweening;
using EventBusSystem;
using Kuhpik;
using UnityEngine;

public class DestroyZoneSecond : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform position_first;
    [SerializeField] private Transform position_second;
    [SerializeField] private Transform position_third;
    [SerializeField] private Transform moneySpawnPosition;

    public void StartDestroy(AttachCarController car, SecondCranController secondCranControlle)
    {
        StartCoroutine(Car(car, secondCranControlle));
    }

    [SerializeField] private int idTestIteration;
    [SerializeField] private bool test;

    private void Awake()
    {
        StartCoroutine(CoolDown());
    }

    private IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(0.3f);
        if (test)
        {
            var newcar = Bootstrap.Instance.GetSystem<CarSpawnSystem>().SpawnCar(idTestIteration);
            StartDestroy(newcar, null);
        }
    }

    private IEnumerator Car(AttachCarController attachCarController, SecondCranController secondCranController)
    {
        var outline = attachCarController.Car.GetComponentsInChildren<Outline>();
        foreach (var outline1 in outline)
        {
            Destroy(outline1);
        }

        var car = attachCarController.Car;
        attachCarController.gameObject.SetActive(false);
        var configuration = attachCarController.AttachCarConfigurations;
        car.parent = null;
        yield return new WaitForSeconds(0.3f);
        var sequence = DOTween.Sequence();
        sequence.Append(car.transform.DOJump(position_first.position, 1, 1, 0.4f));
        sequence.Join(car.transform.DORotateQuaternion(position_first.rotation, 1));
        sequence.Append(car.DOScale(configuration.DestroyScale,
            2f - configuration.StartScaleSecondTime));
        sequence.Join(DOTween.To(() => attachCarController.MeshRenderer.GetBlendShapeWeight(0),
            x => attachCarController.MeshRenderer.SetBlendShapeWeight(0, x), 100,
            2f - configuration.StartScaleSecondTime));
        sequence.Join(DOVirtual.DelayedCall((2f - configuration.StartScaleSecondTime) / 3, () =>
        {
            VibrationSystem.PlayVibration();
            EventBus.RaiseEvent<ISpawnEffectSignal>(t =>
                t.SpawnEffect(EffectType.EffectExplosion, attachCarController.Car));
            for (int i = 0; i < 5; i++)
            {
                attachCarController.DestroyWheel();
            }
        }));
        sequence.Append(DOVirtual.DelayedCall(2f, () =>
        {
            attachCarController.SpawnEffecT();
            if (secondCranController != null) secondCranController.Finish();
        }));
        sequence.Append(car.DOMove(position_second.position, 3f));
        sequence.Append(car.DOMove(position_third.position, 3f));
        sequence.OnComplete(() =>
        {
            var mney = configuration.DestroySecondPreccPrice;
            if (Bootstrap.Instance.PlayerData.IsTutorialFinish == false)
            {
                Bootstrap.Instance.PlayerData.Money = 45;
                EventBus.RaiseEvent<IUpdateMoney>(t => t.UpdateMoney());
            }
            else
            {
                Bootstrap.Instance.GetSystem<MoneySpawnSystem>()
                    .SpawnMoney(moneySpawnPosition, 5, mney);
            }

            EventBus.RaiseEvent<ISpawnEffectSignal>(t => t.SpawnEffect(EffectType.DestroyCar, moneySpawnPosition));
            Destroy(attachCarController.gameObject);
            Destroy(car.gameObject);
        });
        yield return new WaitForSeconds(0.7f);
        animator.SetTrigger("Play");
    }
}