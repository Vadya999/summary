using System;
using System.Collections;
using DG.Tweening;
using Kuhpik;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SecondCranController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private GameObject camera;
    [SerializeField] private Transform carPosition;
    [SerializeField] private Transform carMovePosition;

    [SerializeField] private float rotateSpeed;
    [SerializeField] private Button attachButton;
    [SerializeField] private Animator cranAnimator;

    private Vector2 delta;
    private Vector2 lastPos;

    private float currentCranAnimationValue;
    private bool isCarOnPosition;

    private AttachCarController currentAttachCar;

    private PanelController panelController;
    private DestroyZoneSecond destroyZoneSecond;
    private AttachCarQueueController attachCarQueueController;

    private readonly int rotate = Animator.StringToHash("Rotate");

    private void Awake()
    {
        attachCarQueueController = FindObjectOfType<AttachCarQueueController>();
        panelController = FindObjectOfType<PanelController>();
        destroyZoneSecond = FindObjectOfType<DestroyZoneSecond>();
    }

    private void OnEnable()
    {
        attachButton.onClick.AddListener(ButtonClick);
    }

    private void OnDisable()
    {
        attachButton.onClick.RemoveListener(ButtonClick);
    }

    private void Update()
    {
        Debug.Log("Set!");
        if (panelController.CurrentY != 0.5f)
        {
            var currentFloat = cranAnimator.GetFloat(rotate);
            var speed = panelController.CurrentY > 0.5f ? panelController.CurrentY : -(0.5f - panelController.CurrentY);
            Debug.Log(speed + " " + panelController.CurrentY);
            currentFloat += rotateSpeed * speed * Time.deltaTime;
            if (currentFloat > 1) currentFloat = 1;
            if (currentFloat < 0) currentFloat = 0;
            cranAnimator.SetFloat(rotate, currentFloat);
        }
    }

    public void ButtonClick()
    {
        panelController.Click();
        if (isCarOnPosition)
        {
            var currentValue = cranAnimator.GetFloat(rotate);
            if (currentValue < 0.1f)
            {
                currentAttachCar.Car.transform.parent = carMovePosition;
                currentAttachCar.Car.transform.DOLocalMove(Vector3.zero, 0.2f);
                currentAttachCar.Car.transform.DOLocalRotate(Vector3.zero, 0.2f);
                isCarOnPosition = false;
            }
        }
        else
        {
            if (currentAttachCar != null)
            {
                isCarOnPosition = false;
                destroyZoneSecond.StartDestroy(currentAttachCar, this);
                currentAttachCar = null;
            }
        }
    }

    public void StartDestroy()
    {
        camera.SetActive(true);
        TrySpawnCar();
    }

    public void Finish()
    {
        if (attachCarQueueController.IsCanBeDetachDestoryCar == false)
        {
            camera.SetActive(false);
            Bootstrap.Instance.ChangeGameState(GameStateID.Game);
            Bootstrap.Instance.GetSystem<CameraSystem>().SetCamera(CameraType.Idle);
        }
        else
        {
            TrySpawnCar();
        }
    }

    public void TrySpawnCar()
    {
        if (attachCarQueueController.IsCanBeDetachDestoryCar && currentAttachCar==null)
        {
            Debug.Log("TryComplete");
            isCarOnPosition = true;
            var detachCar = attachCarQueueController.GetCarWithDestroy(true);
            var attachCar = detachCar.TransformObject.GetComponent<AttachCarController>();
            attachCar.AttachPause(9999);
            attachCar.Car.transform.parent = null;
            attachCar.Car.DOJump(carPosition.position, 1, 1, 0.4f);
            attachCar.Car.DORotateQuaternion(carPosition.rotation, 0.4f);
            currentAttachCar = attachCar;
            attachCarQueueController.Recalculate();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Drag");
        delta = lastPos - eventData.position;
        lastPos = eventData.position;
        panelController.Addy(delta.x);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        panelController.SetActivate(true);
        lastPos = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        panelController.SetActivate(false);
        delta = Vector2.zero;
    }
}