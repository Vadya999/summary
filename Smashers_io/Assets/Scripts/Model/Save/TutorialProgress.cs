using System;

[Serializable]
public class TutorialProgress
{
    public bool completed;
    public int stageID;

    public TutorialProgress(bool completed, int stageID)
    {
        this.completed = completed;
        this.stageID = stageID;
    }
}
