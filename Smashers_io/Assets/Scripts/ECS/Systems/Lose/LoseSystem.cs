using Kuhpik;
using System.Linq;
using UnityEngine.Events;
using UnityTools.Extentions;

public class LoseSystem : GameSystemWithScreen<LoseScreen>
{
    private NeighborComponent neighbor => game.activeRoom.neighbors.First();
    private PlayerComponent playerComponent => game.player;

    public readonly UnityEvent RoomFailed = new UnityEvent();

    public override void OnInit()
    {
        RoomFailed?.Invoke();
        screen.RetryButtonClicked.AddListener(LoadLobby);
        this.Invoke(screen.Show, 2.5f);
        ShowLoseAnimations();
    }

    private void ShowLoseAnimations()
    {
        playerComponent.aniamtion.ShowLose();
        neighbor.animation.ShowLose();
        var toNeighborDirection = neighbor.transform.position - playerComponent.transform.position;
        playerComponent.transform.forward = toNeighborDirection;
        neighbor.transform.forward = -toNeighborDirection;
    }

    private void LoadLobby()
    {
        player.SetRoomToLoad(game.activeRoomID);
        Save();
        ReloadScene();
    }
}
