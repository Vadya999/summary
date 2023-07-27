using System;

[Serializable]
public class AnalyticsProgress
{
    public bool logedFirstLaunched;
    public int logedLevelStartedID = -1;
    public int logedLevelCompltedID = -1;

    public bool logedTutorialStarted;
    public int logedTutorialStepCompleted = -1;
    public bool logedTutorialComplted;
}