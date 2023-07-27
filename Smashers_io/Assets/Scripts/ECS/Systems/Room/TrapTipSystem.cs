using Cinemachine;
using Kuhpik;
using System;
using System.Collections;
using UnityEngine;
using UnityTools;

public class TrapTipSystem : GameSystem
{
    private TargetPointerSystem _targetPointer;
    private TutorialSystem _tutorialSystem;
    private PlayerMovementSystem _playerMovementSystem;
    private WardrobeEnterSystem _wardrobeSystem;

    private readonly Timer _tipTimer = new Timer(10f);
    private readonly Timer _idleTimer = new Timer(5f);

    private CinemachineVirtualCamera playerCamera => game.activeCamera;

    public bool isShowingTip;

    private bool _pointed;

    public override void OnInit()
    {
        _tutorialSystem = GetSystem<TutorialSystem>();
        _targetPointer = GetSystem<TargetPointerSystem>();
        _playerMovementSystem = GetSystem<PlayerMovementSystem>();
        _wardrobeSystem = GetSystem<WardrobeEnterSystem>();
        _tipTimer.Reset();
    }

    public override void OnUpdate()
    {
        if (_tutorialSystem.isTutorialHasText) return;
        UpdateIdleTimer();
        UpdateTimer();
        UpdateTarget();
    }

    private void UpdateIdleTimer()
    {
        if (!_playerMovementSystem.isMoving)
        {
            _idleTimer.UpdateTimer();
        }
        else
        {
            _idleTimer.Reset();
        }
    }

    private void UpdateTarget()
    {
        if (_tipTimer.isReady)
        {
            if (_idleTimer.isReady || _wardrobeSystem.inWardrobe)
            {
                var trap = GetTrap(game.player.activeItem);
                _targetPointer.SetTarget(trap.transform);
                if (!_pointed)
                {
                    MoveCameraToPoint(trap.transform);
                    StartCoroutine(Routine());
                    _pointed = true;
                }
            }
        }
        else
        {
            _pointed = false;
            _targetPointer.SetTarget(null);
        }
    }

    private IEnumerator Routine()
    {
        isShowingTip = true;
        yield return new WaitForSeconds(3);
        isShowingTip = false;
    }

    public void MoveCameraToPoint(Transform point, Action onComplete)
    {
        playerCamera.StopAllCoroutines();
        playerCamera.StartCoroutine(ShowRoutine());
        IEnumerator ShowRoutine()
        {
            game.SetCameraTarget(point);
            yield return new WaitForSeconds(3);
            game.SetPlayerAsCameraTarget();
            onComplete?.Invoke();
        }
    }

    public void MoveCameraToPoint(Transform point)
    {
        MoveCameraToPoint(point, null);
    }

    private void UpdateTimer()
    {
        if (game.player.activeItem != null)
        {
            _tipTimer.UpdateTimer();
        }
        else
        {
            _tipTimer.Reset();
        }
    }

    private TrapComponent GetTrap(ItemComponent item)
    {
        var traps = game.activeRoom.traps;
        for (int i = 0; i < traps.Length; i++)
        {
            var trap = traps[i];
            if (trap.requiredItem.itemType == item.itemType)
            {
                return trap;
            }
        }
        return null;
    }
}
