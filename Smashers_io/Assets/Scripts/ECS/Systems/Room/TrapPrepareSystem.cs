using Kuhpik;
using UnityTools;

public class TrapPrepareSystem : GameSystem
{
    private RoomComponent activeRoom => game.activeRoom;
    private PlayerComponent playerComponent => game.player;

    private Timer _interactonTimer;

    private TrapComponent _nearTrap;

    private PlayerMovementSystem _playerMovementSystem;

    private Timer _idleTimer = new Timer(0.25f);

    public override void OnInit()
    {
        _playerMovementSystem = GetSystem<PlayerMovementSystem>();
        _interactonTimer = new Timer(config.trapPrepareTime);
        _interactonTimer.Reset();
    }

    public override void OnUpdate()
    {
        UpdateIdle();
        UpdatePrepare();
        UpdateInteractionProgress();
        UpdateInteractionBubbles();
    }

    private void UpdateInteractionProgress()
    {
        if (_nearTrap != null)
        {
            _nearTrap.interactionProgress.SetProgress(_interactonTimer.normalized);
        }
    }

    private void UpdateInteractionBubbles()
    {
        for (int i = 0; i < game.activeRoom.traps.Length; i++)
        {
            var trap = game.activeRoom.traps[i];
            var shouldShowBubble = CanPrepareTrap(trap);
            trap.interactionBubble.gameObject.SetActive(shouldShowBubble);
        }
    }

    private void UpdatePrepare()
    {
        if (_nearTrap != null && CanPrepareTrap(_nearTrap) && _idleTimer.isReady)
        {
            _interactonTimer.UpdateTimer();
            playerComponent.aniamtion.SetTrapping(true, _nearTrap.trapAnimation);
        }
        else
        {
            playerComponent.aniamtion.SetTrapping(false, PlayerTrapID.Default);
            _interactonTimer.Reset();
        }
        if (_interactonTimer.isReady)
        {
            Destroy(playerComponent.activeItem.gameObject);
            playerComponent.activeItem = null;
            _nearTrap.Prepare();
        }
    }

    private void UpdateIdle()
    {
        if (_playerMovementSystem.isMoving)
        {
            _idleTimer.Reset();
        }
        else
        {
            _idleTimer.UpdateTimer();
        }
    }

    public override void OnStateEnter()
    {
        foreach (var trap in activeRoom.traps)
        {
            trap.trigger.PlayerEntered.AddListener(OnTrapTriggerEntered);
            trap.trigger.PlayerExited.AddListener(OnTrapTriggerExited);
        }
    }

    public override void OnStateExit()
    {
        foreach (var trap in activeRoom.traps)
        {
            trap.trigger.PlayerEntered.RemoveListener(OnTrapTriggerEntered);
            trap.trigger.PlayerExited.RemoveListener(OnTrapTriggerExited);
        }
    }

    private void OnTrapTriggerEntered(TrapComponent trap)
    {
        _nearTrap = trap;
    }

    private void OnTrapTriggerExited(TrapComponent trap)
    {
        trap.interactionProgress.SetProgress(0);
        _nearTrap = null;
    }

    private bool CanPrepareTrap(TrapComponent trap)
    {
        return playerComponent.activeItem != null && trap.requiredItem.itemType == playerComponent.activeItem.itemType && !trap.preparedByPlayer;
    }
}