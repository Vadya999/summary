using Kuhpik;
using UnityEngine.Events;
using UnityTools.Extentions;

public class RoomEnterSystem : GameSystemWithScreen<RoomEnterScreen>
{
    private LevelComponent level => game.level;
    private PlayerMovementComponent playerMovement => game.player.movement;

    private RoomComponent _nearRoomEnter;

    private void TryForceLoadRoom()
    {
        if (player.ShouldLoadRoom(out var roomID))
        {
            _nearRoomEnter = game.level.rooms[roomID];
            Save();
            EnterRoom();
        }
    }

    public override void OnInit()
    {
        this.Invoke(TryForceLoadRoom, 0.25f);
    }

    public override void OnStateEnter()
    {
        screen.EnterButtonClicked.AddListener(EnterRoom);
        foreach (var room in level.rooms)
        {
            room.enterTrigger.PlayerEntered.AddListener(OnDoorEnter);
            room.enterTrigger.PlayerExited.AddListener(OnDoorExit);
        }
    }

    public override void OnStateExit()
    {
        screen.EnterButtonClicked.RemoveListener(EnterRoom);
        foreach (var room in level.rooms)
        {
            room.enterTrigger.PlayerEntered.RemoveListener(OnDoorEnter);
            room.enterTrigger.PlayerExited.RemoveListener(OnDoorExit);
        }
    }

    public override void OnUpdate()
    {
        screen.SetState(_nearRoomEnter != null, "ENTER");
        if (_nearRoomEnter != null)
        {
            var starCount = player.progress.GetProgress(player.currentLevelID, game.level.rooms.IndexOf(_nearRoomEnter)).starCount;
            if (starCount > 0)
            {
                screen.SetState(true, "TRY AGAIN");
            }
        }
    }

    private void EnterRoom()
    {
        if (_nearRoomEnter == null) return;
        player.SetPlayerPosition(game.player.transform.position);
        playerMovement.transform.position = _nearRoomEnter.startPoint.position;
        game.activeRoom = _nearRoomEnter;
        game.SetActiveCamera(_nearRoomEnter.virtualCamera);
        _nearRoomEnter = null;
        ChangeGameState(GameStateID.Room);
    }

    private void OnDoorEnter(RoomComponent room)
    {
        _nearRoomEnter = room;
    }

    private void OnDoorExit(RoomComponent room)
    {
        _nearRoomEnter = null;
    }
}
