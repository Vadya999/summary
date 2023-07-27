using Cinemachine;
using Kuhpik;
using UnityEngine;

public class LevelLoadingSystem : GameSystem
{
    public bool canChangeLevel => game.GetStarCount(player.currentLevelID) >= config.starRequiredToNextLevel[player.currentLevelID];

    public override void OnInit()
    {
        LoadLevel();
        game.SetActiveCamera(game.level.virtaulCamera);
        LoadPlayer();
    }

    private void LoadPlayer()
    {
        game.player = FindObjectOfType<PlayerComponent>();
    }

    public void LoadNextLevel()
    {
        player.currentLevelID++;
        Save();
        ReloadScene();
    }

    public void TryLoadNextLevel()
    {
        if (canChangeLevel)
        {
            LoadNextLevel();
        }
    }

    private void LoadLevel()
    {
        player.progress ??= new GameProgress();
        var levelID = GetLevelID(player.currentLevelID);
        var levelPrefab = Resources.Load<LevelComponent>($"Levels/{levelID}");
        game.level = Instantiate(levelPrefab);
    }

    private int GetLevelID(int currentID)
    {
        if (currentID < config.levelCount)
        {
            return currentID;
        }
        else
        {
            var random = new System.Random(currentID);
            return random.Next(config.levelCount);
        }
    }
}
