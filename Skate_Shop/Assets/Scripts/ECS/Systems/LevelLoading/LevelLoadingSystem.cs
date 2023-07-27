using Kuhpik;
using System.Collections;
using UnityEngine;

public class LevelLoadingSystem : GameSystem
{
    public override void OnStateEnter()
    {
        LoadLevel();
    }

    private void LoadLevel()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        if (game.level != null)
        {
            Destroy(game.level.gameObject);
            yield return null;
        }
        var levelPrefab = GetLevelPrefab();
        game.level = Instantiate(levelPrefab);
        FindLevelObjects();
        BakeNavMesh();
        ChangeGameState(GameStateID.SaveRestore);
    }

    private LevelComponent GetLevelPrefab()
    {
        var levelPath = $"Levels/Level {game.levelID + 1}";
        return Resources.Load<LevelComponent>(levelPath);
    }

    private void BakeNavMesh()
    {
        game.level.segments.ForEach(x => x.navMesh.Build());
    }

    private void FindLevelObjects()
    {
        GameData.tutorialScreen = UIManager.GetUIScreen<TutorialScreen>();
        GameData.upgradeScreen = UIManager.GetUIScreen<UpgradeScreen>();
        GameData.nextLevelScreen = UIManager.GetUIScreen<NextLevelScreen>();

        GameData.saveSystem = GetSystem<LoadSystem>();

        GameData.player = game.level.player;
    }
}
