using Kuhpik;
using UnityEngine;

public class CheatsSystem : GameSystem
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            player.progress.SetProgress(player.currentLevelID, 0, new RoomProgress(3));
            player.progress.SetProgress(player.currentLevelID, 1, new RoomProgress(3));
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            game.tutorialProgress.completed = true;
            GetSystem<TutorialLoadingSytem>().SaveTutorial();
            ReloadScene();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            player.currentLevelID++;
            ReloadScene();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            player.currentLevelID = Mathf.Max(player.currentLevelID - 1, 0);
            ReloadScene();
        }
    }
}
