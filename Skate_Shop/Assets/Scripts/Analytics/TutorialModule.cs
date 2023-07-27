using GameAnalyticsSDK;

public class TutorialModule
{
    public void StepComplete(int stepID, string name)
    {
        var newName = name.Replace(' ', '_').ToLower();
        GameAnalytics.NewDesignEvent($"tutorial_step{stepID + 1}_{newName}");
    }

    public void TutorialStarted()
    {
        GameAnalytics.NewDesignEvent("tutorial_started");
    }

    public void TutorialCompleted()
    {
        GameAnalytics.NewDesignEvent("tutorial_completed");
    }
}
