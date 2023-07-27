using System;

[Serializable]
public class SerializableTutorial
{
    public bool completed;
    public int stepID;

    public SerializableTutorial()
    {
        completed = true;
        stepID = 0;
    }

    public SerializableTutorial(bool completed, int stepID)
    {
        this.completed = completed;
        this.stepID = stepID;
    }
}

