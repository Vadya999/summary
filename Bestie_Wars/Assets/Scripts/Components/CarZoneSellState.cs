using System.Collections;
using DG.Tweening;
using Kuhpik;
using StateMachine;
using UnityEngine;

public class CarZoneSellState : State
{
    private readonly GameObject sellImage;
    private AttachCarController attachCarController;

    public bool IsReadyToLeave;

    public CarZoneSellState(GameObject sellImage)
    {
        this.sellImage = sellImage;
    }

    public void AttachCarController(AttachCarController attachCarController)
    {
        this.attachCarController = attachCarController;
    }

    public override void OnStateEnter()
    {
        sellImage.SetActive(true);
        sellImage.transform.DOShakeScale(0.5f, sellImage.transform.localScale.x);
        Bootstrap.Instance.GetSystem<CoroutineSystem>().StartCoroutine(SellAnimation());
    }

    public override void OnStateExit()
    {
        sellImage.SetActive(false);
        IsReadyToLeave = false;
    }

    private IEnumerator SellAnimation()
    {
        if (Bootstrap.Instance.PlayerData.IsTutorialFinish)
        {
            yield return new WaitForSeconds(Random.Range(0f, 8f));
        }
        var positions = Bootstrap.Instance.GetSystem<PositionsSystem>();
        var destoryCar = attachCarController;
        var positionMiniPlayer = positions.SpawnMiniCharacterPosition;
        attachCarController.MiniPlayer.transform.position = positionMiniPlayer.position;
        attachCarController.MiniPlayer.gameObject.SetActive(true);
        attachCarController.MiniPlayer.transform.DOShakeScale(0.3f,
            attachCarController.MiniPlayer.transform.localScale.x);
        attachCarController.MiniPlayer.MoveToPosition(attachCarController.transform);
        while (attachCarController.MiniPlayer.OnPosition == false)
        {
            yield return new WaitForSeconds(0);
        }
        
        var addMoney = Bootstrap.Instance.PlayerData.IsTutorialFinish == false ? 50 : attachCarController.AttachCarConfigurations.SellPrice;
        Bootstrap.Instance.GetSystem<MoneySpawnSystem>().SpawnMoney(attachCarController.transform, 5,
            addMoney);
        IsReadyToLeave = true;
        attachCarController.MiniPlayer.OnPosition = false;
        attachCarController.MiniPlayer.gameObject.SetActive(false);
        var sequence = DOTween.Sequence();
        var speed = 3f;
        var transform = attachCarController.transform;
        var position = transform.position;
        var pos = new Vector3(position.x, position.y,
            positions.CarMovePositionFirst.position.z);
        var pos2 = new Vector3(positions.CarMovePositionSecond.position.x, position.y,
            positions.CarMovePositionSecond.position.z);
        var pos3 = new Vector3(positions.CarMovePositionThird.position.x, position.y,
            positions.CarMovePositionThird.position.z);
        sequence.Append(attachCarController.transform.DOMove(pos, speed));
        sequence.Join(attachCarController.transform.DOLookAt(pos, 0.3f));
        sequence.Append(attachCarController.transform.DOMove(pos2, speed));
        sequence.Join(attachCarController.transform.DOLookAt(pos2, 0.3f));
        sequence.Append(attachCarController.transform.DOMove(pos3, speed));
        sequence.Join(attachCarController.transform.DOLookAt(pos3, 0.3f))
            .OnComplete(() => GameObject.Destroy(destoryCar.gameObject));
    }
}