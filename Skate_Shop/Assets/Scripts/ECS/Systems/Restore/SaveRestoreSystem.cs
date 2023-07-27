using Kuhpik;

public class SaveRestoreSystem : GameSystem
{
    public override void OnStateEnter()
    {
        GetSystem<LoadSystem>()
            .saveSegments
            .ForEach(x => x.bootstrap = Bootstrap.Instance);

        if (game.shouldRestore) RestoreState();
        ChangeGameState(GameStateID.Game);
    }

    private void RestoreState()
    {
        GetSystem<LoadSystem>()
            .saveSegments
            .ForEach(x => x.Load(game.saveData));
        game.shouldRestore = false;
    }
}
