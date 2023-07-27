using System;
using System.Collections;
using DG.Tweening;
using Kuhpik;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeTrigger : PlayerTriggerZone
{
    [SerializeField] private Transform car;
    [SerializeField] private Transform carPos;
    [SerializeField] private Image image;
    [SerializeField] private float time;

    private bool isTriggerActivated;
    private Vector3 startPos;
    private Quaternion startRotate;
    private float currentTime;

    protected override void AwakeFake()
    {
     
    }

    private void Start()
    {
        StartCoroutine(Wait());
    }

    public void Leave()
    {
        car.transform.localPosition = startPos;
        car.transform.localRotation = startRotate;
        Bootstrap.Instance.ChangeGameState(GameStateID.Game);
        Bootstrap.Instance.GetSystem<CameraSystem>().SetCamera(CameraType.Idle);
        car.transform.DOShakeScale(0.3f, 0.5f);
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
        if (Bootstrap.Instance.PlayerData.IsTutorialFinish == false) gameObject.SetActive(false);
    }
    private void Update()
    {
        if (currentTime >= time && isTriggerActivated == false)
        {
            isTriggerActivated = true;
            Activate();
        }

        if (IsPlayerInZone && currentTime != time)
        {
            currentTime += Time.deltaTime;
            if (currentTime > time) currentTime = time;
        }
        else
        {
            if (IsPlayerInZone == false && currentTime != 0)
            {
                currentTime -= Time.deltaTime;
                isTriggerActivated = false;
                if (currentTime < 0) currentTime = 0;
            }
        }

        image.fillAmount = currentTime / time;
    }

    private void Activate()
    {
        startPos = car.localPosition;
        startRotate = car.localRotation;
        car.transform.position = carPos.position;
        car.transform.rotation = carPos.rotation;
        car.transform.DOShakeScale(0.3f, 0.5f);
        Bootstrap.Instance.ChangeGameState(GameStateID.Menu);
        Bootstrap.Instance.GetSystem<CameraSystem>().SetCamera(CameraType.Upgrade);
    }
}