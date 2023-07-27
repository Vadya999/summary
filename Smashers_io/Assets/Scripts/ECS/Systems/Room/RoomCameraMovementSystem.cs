using Kuhpik;
using System.Linq;
using UnityEngine;
using UnityTools.Extentions;

public class RoomCameraMovementSystem : GameSystem
{
    private PlayerComponent playerComponent => game.player;

    private Bounds _roomBounds;

    private WardrobeEnterSystem _wardrobeEnterSystem;

    public override void OnInit()
    {
        _wardrobeEnterSystem = GetSystem<WardrobeEnterSystem>();
    }

    public override void OnStateEnter()
    {
        RecalculateRoomsBounds();
    }

    public override void OnUpdate()
    {
        if (_wardrobeEnterSystem.inWardrobe)
        {
            MoveToCenter();
        }
        else
        {
            MoveToPlayer();
        }
    }

    private void MoveToCenter()
    {
        var centerPosition = playerComponent.transform.position;
        centerPosition.x = game.activeRoom.neighbors.First().transform.position.x;
        centerPosition.y = game.activeRoom.neighbors.First().transform.position.y;
        playerComponent.cameraTarget.position = centerPosition;
    }

    private void MoveToPlayer()
    {
        var playerPosition = playerComponent.transform.position;
        var playerPositionY = playerPosition.y;
        var centerY = _roomBounds.center.y;
        var lerpedValueY = Mathf.Lerp(playerPositionY, centerY, 0.5f);

        var lerpedValue = new Vector3(playerPosition.x, lerpedValueY, playerPosition.z);
        playerComponent.cameraTarget.position = lerpedValue;
    }

    private void RecalculateRoomsBounds()
    {
        var roomsRoot = game.activeRoom.transform.GetChildrens().First(x => x.name == "Rooms");
        var rooms = roomsRoot.GetComponentsInChildren<Renderer>();
        _roomBounds = rooms.FirstOrDefault().bounds;
        foreach (var room in rooms)
        {
            _roomBounds.Encapsulate(room.bounds);
        }
        MathExtensions.DrawBounds(_roomBounds, Color.red, 10000);
    }

    public override void OnStateExit()
    {
        game.player.cameraTarget.localPosition = Vector3.zero;
    }
}
