using System;
using System.Collections;
using DG.Tweening;
using EventBusSystem;
using Kuhpik;
using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform cranTransformPosition;
    [SerializeField] private Transform positionFirst;
    [SerializeField] private Transform positionSecond;
    [SerializeField] private Transform positionThird;
    [SerializeField] private Transform moneySpawnPos;

    private AttachCarController attachCar;
    public AttachCarController AttachCarController => attachCar;

    public bool IsCanBeAttach => attachCar == null;

    public void StartDestroy(AttachCarController attach)
    {
        attach.Car.transform.DOJump(cranTransformPosition.position, 4, 1, 0.4f);
        attach.Car.transform.DORotateQuaternion(cranTransformPosition.rotation, 0.4f);
        attachCar = attach;
        attach.AttachPause(9999);
        Bootstrap.Instance.ChangeGameState(GameStateID.CranController);
        Bootstrap.Instance.GetSystem<CameraSystem>().SetCamera(CameraType.Cran);
    }

    public void DestroyStart()
    {
        StartCoroutine(Destroy());
    }

    private void Awake()
    {
        StartCoroutine(Play());
    }

    private IEnumerator Play()
    {
        yield return new WaitForSeconds(0.5f);
        if (Bootstrap.Instance.PlayerData.IsTutorialFinish)
            animator.SetTrigger("PlayTs");
    }

    private IEnumerator Destroy()
    {
        var attach = attachCar;
        attach.GetComponent<Collider>().isTrigger = true;
        attach.AttachPause(120);
        var outline = attachCar.Car.GetComponentsInChildren<Outline>();
        foreach (var outline1 in outline)
        {
            Destroy(outline1);
        }

        attachCar.Car.transform.DOMove(positionFirst.position, 0.1f);
        attachCar.Car.transform.DORotateQuaternion(positionFirst.rotation, 0.1f);
        attachCar.Car.transform.DOShakeScale(0.2f, new Vector3(0, 0.2f, 0));
        var transformCar = attachCar.Car.transform;
        var configuration = attachCar.AttachCarConfigurations;
        animator.SetTrigger("Play");
        yield return new WaitForSeconds(attachCar.AttachCarConfigurations.StartScaleTime);
        var scale = DOTween.Sequence();
        scale.Append(transformCar.DOScale(configuration.DestroyScale,
            1.67f - configuration.StartScaleTime));
        scale.Join(DOTween.To(() => attachCar.MeshRenderer.GetBlendShapeWeight(0),
            x => attachCar.MeshRenderer.SetBlendShapeWeight(0, x), 100, 1.67f - configuration.StartScaleTime));
        scale.Join(DOVirtual.DelayedCall((1.67f - configuration.StartScaleTime) / 3, () =>
        {
            VibrationSystem.PlayVibration();
            EventBus.RaiseEvent<ISpawnEffectSignal>(t => t.SpawnEffect(EffectType.EffectExplosion, attachCar.Car));
            for (int i = 0; i < 5; i++)
            {
                attachCar.DestroyWheel();
            }
        }));
        scale.Append(DOVirtual.DelayedCall(2f, () =>
        {
            attachCar.SpawnEffecT();
            attachCar = null;
            StartCoroutine(WaitChangeGame());
        }));
        scale.Append(transformCar.DOMove(positionSecond.position, 3f));
        scale.Append(transformCar.DOMove(positionThird.position, 3f));
        scale.OnComplete(() =>
        {
            var mney = configuration.DestroyPrice;
            if (Bootstrap.Instance.PlayerData.IsTutorialFinish == false)
            {
                Bootstrap.Instance.PlayerData.Money = 45;
                EventBus.RaiseEvent<IUpdateMoney>(t => t.UpdateMoney());
            }
            else
            {
                Bootstrap.Instance.GetSystem<MoneySpawnSystem>()
                    .SpawnMoney(moneySpawnPos, 5, mney);
            }

            EventBus.RaiseEvent<ISpawnEffectSignal>(t => t.SpawnEffect(EffectType.DestroyCar, moneySpawnPos));
            Destroy(attach.gameObject);
            Destroy(transformCar.gameObject);
        });
    }

    private IEnumerator WaitChangeGame()
    {
        yield return new WaitForSeconds(1f);
        if (attachCar != null) yield break;
        Bootstrap.Instance.ChangeGameState(GameStateID.Game);
        Bootstrap.Instance.GetSystem<CameraSystem>().SetCamera(CameraType.Idle);
    }
}