using Kuhpik;
using UnityEngine;

public class WardrobeEnterSystem : GameSystemWithScreen<WardrobeScreen>
{
    private RoomComponent activeRoom => game.activeRoom;
    private PlayerMovementComponent playerMovement => game.player.movement;

    private WardrobeComponent _nearWardrobe;
    private WardrobeComponent _activeWardrobe;

    public bool inWardrobe => _activeWardrobe != null;

    public override void OnStateEnter()
    {
        screen.WardrobeButtonClicked.AddListener(ChangeState);
        foreach (var wardrobe in activeRoom.wardrobes)
        {
            wardrobe.trigger.PlayerEntered.AddListener(OnPlayerEntered);
            wardrobe.trigger.PlayerExited.AddListener(OnPlayerExit);
        }
    }

    public override void OnStateExit()
    {
        screen.WardrobeButtonClicked.RemoveListener(ChangeState);
        foreach (var wardrobe in activeRoom.wardrobes)
        {
            wardrobe.trigger.PlayerEntered.RemoveListener(OnPlayerEntered);
            wardrobe.trigger.PlayerExited.RemoveListener(OnPlayerExit);
        }
    }

    private void Update()
    {
        screen.SetState(_nearWardrobe != null || inWardrobe);
        screen.hideText.text = inWardrobe ? "EXIT" : "HIDE";
    }

    public void ForceExitWardrobe()
    {
        if (inWardrobe)
        {
            ExitWardrobe();
        }
    }

    private void ChangeState()
    {
        if (inWardrobe)
        {
            ExitWardrobe();
        }
        else
        {
            EnterWardrobe();
        }
    }

    private void EnterWardrobe()
    {
        _activeWardrobe = _nearWardrobe;
        PlayerInput.isEnabled = false;
        playerMovement.SetPhysicsState(false);
        playerMovement.transform.position = _activeWardrobe.inPoint.position;
        playerMovement.transform.rotation = _activeWardrobe.inPoint.rotation;
    }

    private void ExitWardrobe()
    {
        PlayerInput.isEnabled = true;
        playerMovement.SetPhysicsState(true);
        playerMovement.transform.position = _activeWardrobe.exitPoint.position;
        playerMovement.transform.rotation = _activeWardrobe.exitPoint.rotation;
        _activeWardrobe = null;
    }

    private void OnPlayerEntered(WardrobeComponent wardrobe)
    {
        _nearWardrobe = wardrobe;
    }

    private void OnPlayerExit(WardrobeComponent wardrobe)
    {
        _nearWardrobe = null;
    }
}
