using Ketchapp.MayoSDK;
using Ketchapp.MayoSDK.Analytics;
using Kuhpik;
using UnityTools;

public class SDKEventsSystem : GameSystem
{
    private AnalyticsManager analytics => KetchappSDK.Analytics;
    private AnalyticsProgress progress => player.analitycsProgress;

    private readonly Timer _failEventCD = new Timer(5f);

    public override void OnInit()
    {
        _failEventCD.End();
        player.analitycsProgress ??= new AnalyticsProgress();
        LogFirstGameLaunch();
    }

    public override void OnGameStart()
    {
        GetSystem<WinSystem>().RoomCompleted.AddListener(LogRoomComplted);
        GetSystem<LoseSystem>().RoomFailed.AddListener(LogRoomFailed);

        GetSystem<ElevatorSystem>().LevelStarted.AddListener(LogLevelStart);
        GetSystem<ElevatorSystem>().LevelComplted.AddListener(LogLevelCompleted);

        GetSystem<TutorialSystem>().Started.AddListener(LogTutorialStarted);
        GetSystem<TutorialSystem>().StepCompleted.AddListener(LogTutorialStepCompleted);
        GetSystem<TutorialSystem>().Completed.AddListener(LogTutorialCompleted);
    }

    public override void OnGameEnd()
    {
        GetSystem<WinSystem>().RoomCompleted.RemoveListener(LogRoomComplted);
        GetSystem<LoseSystem>().RoomFailed.RemoveListener(LogRoomFailed);

        GetSystem<ElevatorSystem>().LevelStarted.RemoveListener(LogLevelStart);
        GetSystem<ElevatorSystem>().LevelComplted.RemoveListener(LogLevelCompleted);

        GetSystem<TutorialSystem>().Started.RemoveListener(LogTutorialStarted);
        GetSystem<TutorialSystem>().StepCompleted.RemoveListener(LogTutorialStepCompleted);
        GetSystem<TutorialSystem>().Completed.RemoveListener(LogTutorialCompleted);
    }

    public override void OnAlwaysUpdate()
    {
        _failEventCD.UpdateTimer();
    }

    private void LogFirstGameLaunch()
    {
        if (!progress.logedFirstLaunched)
        {
            analytics.CustomEvent("first_game_launch");
            progress.logedFirstLaunched = true;
            Save();
        }
    }

    private void LogRoomFailed() => LogRoomFailed(player.currentLevelID + 1, game.activeRoomID + 1);
    private void LogRoomFailed(int levelID, int roomID)
    {
        if (_failEventCD.isReady)
        {
            analytics.CustomEvent($"fail_lvl{levelID}_room{roomID}");
            _failEventCD.Reset();
        }
    }

    private void LogRoomComplted() => LogRoomComplted(player.currentLevelID + 1, game.activeRoomID + 1);
    private void LogRoomComplted(int levelID, int roomID)
    {
        analytics.CustomEvent($"level{levelID}_room{roomID}");
    }

    private void LogLevelStart() => LogLevelStart(player.currentLevelID + 1);
    private void LogLevelStart(int levelID)
    {
        if (levelID > progress.logedLevelStartedID)
        {
            analytics.GetLevel(levelID).ProgressionStart();
            progress.logedLevelStartedID = levelID;
            Save();
        }
    }

    private void LogLevelCompleted() => LogLevelCompleted(player.currentLevelID + 1);
    private void LogLevelCompleted(int levelID)
    {
        if (levelID > progress.logedLevelCompltedID)
        {
            analytics.GetLevel(levelID).ProgressionComplete();
            progress.logedLevelCompltedID = levelID;
            Save();
        }
    }

    private void LogTutorialStarted()
    {
        if (!progress.logedTutorialStarted)
        {
            analytics.CustomEvent("tutorial_started");
            progress.logedTutorialStarted = true;
            Save();
        }
    }

    private void LogTutorialStepCompleted(int stepID, string name)
    {
        if (stepID > progress.logedTutorialStepCompleted)
        {
            analytics.CustomEvent($"tutorial_step{stepID}_{name}");
            progress.logedTutorialStepCompleted = stepID;
            Save();
        }
    }

    private void LogTutorialCompleted()
    {
        if (!progress.logedTutorialComplted)
        {
            analytics.CustomEvent("tutorial_completed");
            progress.logedTutorialComplted = true;
            Save();
        }
    }
}
