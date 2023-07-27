using Kuhpik;

public class TutorialLoadingSytem : GameSystem
{
    public override void OnInit()
    {
        player.tutorialProgress ??= new TutorialProgress(false, 0);
        game.tutorialProgress = new TutorialProgress(player.tutorialProgress.completed, player.tutorialProgress.stageID);

    }

    public void SaveTutorial()
    {
        player.tutorialProgress = new TutorialProgress(game.tutorialProgress.completed, game.tutorialProgress.stageID);
        Save();
    }
}
