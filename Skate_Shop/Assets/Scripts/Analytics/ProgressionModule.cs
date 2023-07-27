using GameAnalyticsSDK;

public class ProgressionModule
{
    public void FirstGameLaunch()
    {
        GameAnalytics.NewDesignEvent("first_game_launch");
        LevelStarted(1);
        SDKEvents.tutorial.TutorialStarted();
    }

    public void LevelStarted(int level)
    {
        GameAnalytics.NewDesignEvent($"level_{level}_started");
    }

    public void LevelCompleted(int level)
    {
        GameAnalytics.NewDesignEvent($"level_{level}_completed");
    }
}
