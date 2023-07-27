using Kuhpik;
using UnityEngine.Events;
using UnityTools.Extentions;

public class WinSystem : GameSystemWithScreen<WinScreen>
{
    private RoomCompletionSystem _completionSystem;

    private int starCount => _completionSystem.GetStarCount();

    public readonly UnityEvent RoomCompleted = new UnityEvent();

    public override void OnInit()
    {
        _completionSystem = GetSystem<RoomCompletionSystem>();
        screen.LobbyButtonClicked.AddListener(LoadLobby);
        screen.RetryButtonClicked.AddListener(ReloadRoom);
    }

    public override void OnStateEnter()
    {
        RoomCompleted?.Invoke();
        game.player.aniamtion.ShowWin(0);
        game.activeRoom.neighbors.ForEach(x => x.animation.ShowWin());
        this.Invoke(ShowWinScreen, 4);
        GetSystem<TutorialLoadingSytem>().SaveTutorial();
    }

    private void ShowWinScreen()
    {
        screen.ShowResult(starCount);
        ApplyStars();
    }

    private void ReloadRoom()
    {
        player.SetRoomToLoad(game.activeRoomID);
        ReloadScene();
    }

    private void LoadLobby()
    {
        ReloadScene();
    }

    private void ApplyStars()
    {
        var roomID = game.activeRoomID;
        var levelID = player.currentLevelID;
        var currentStars = player.progress.GetProgress(levelID, roomID);
        var newProgress = new RoomProgress(starCount);
        if (currentStars.starCount < newProgress.starCount)
        {
            player.progress.SetProgress(levelID, roomID, newProgress);
            Save();
        }
    }
}