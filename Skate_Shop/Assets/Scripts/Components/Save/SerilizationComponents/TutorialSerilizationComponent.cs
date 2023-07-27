public class TutorialSerilizationComponent : SerializationSegment
{
    private TutorialComponent tutorial => FindObjectOfType<TutorialComponent>();

    public override void Load(SaveData saveData)
    {
        if (tutorial != null)
        {
            tutorial.currentStepID = saveData.level.tutorial.stepID;
        }
    }

    public override void Save(SaveData saveData)
    {
        var result = new SerializableTutorial();
        if (tutorial == null)
        {
            result.completed = true;
        }
        else
        {
            result.completed = tutorial.completed;
            result.stepID = tutorial.currentStepID;
        }
        saveData.level.tutorial = result;
    }
}
