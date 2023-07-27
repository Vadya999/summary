using Kuhpik;
using System.Collections;
using UnityEngine;

public class RoomExitSystem : GameSystemWithScreen<RoomEnterScreen>
{
    private RoomComponent activeRoom => game.activeRoom;

    private bool _canExit;

    public override void OnStateEnter()
    {
        screen.EnterButtonClicked.AddListener(OnExitButtonClicked);
        activeRoom.exitTrigger.PlayerEntered.AddListener(OnPlayerEntered);
        activeRoom.exitTrigger.PlayerExited.AddListener(OnPlayerExit);
    }

    public override void OnStateExit()
    {
        screen.EnterButtonClicked.RemoveListener(OnExitButtonClicked);
        activeRoom.exitTrigger.PlayerEntered.RemoveListener(OnPlayerEntered);
        activeRoom.exitTrigger.PlayerExited.RemoveListener(OnPlayerExit);
    }

    public override void OnUpdate()
    {
        screen.SetState(_canExit, "EXIT");
    }

    private void OnPlayerEntered(RoomComponent room)
    {
        _canExit = true;
    }

    private void OnPlayerExit(RoomComponent room)
    {
        _canExit = false;
    }

    private void OnExitButtonClicked()
    {
        StartCoroutine(ExitRoutine());
    }

    private IEnumerator ExitRoutine()
    {
        PlayerInput.isEnabled = false;
        game.player.transform.position = activeRoom.endPoint.position;
        game.SetActiveCamera(game.level.virtaulCamera);
        yield return new WaitForSeconds(1.5f);
        PlayerInput.isEnabled = true;
        player.SetPlayerPosition(game.player.transform.position);
        Save();
        ReloadScene();
    }
}