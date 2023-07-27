using Kuhpik;

public class DoorsSystem : GameSystem
{
    private RoomComponent activeRoom => game.activeRoom;
    private PlayerMovementComponent playerMovement => game.player.movement;

    public override void OnStateEnter()
    {
        foreach (var door in activeRoom.doors)
        {
            door.trigger.PlayerEntered.AddListener(UseDoor);
        }
    }

    public override void OnStateExit()
    {
        foreach (var door in activeRoom.doors)
        {
            door.trigger.PlayerEntered.RemoveListener(UseDoor);
        }
    }
    private void UseDoor(DoorComponent door)
    {
        if (!door.enterLock)
        {
            StartCoroutine(door.MoveRoutine(playerMovement.transform, game.player.aniamtion));
        }
    }
}
