using Kuhpik;
using System.Collections;

public class PlayerLoadingSystem : GameSystem
{
    public override void OnInit()
    {
        PlayerInput.isEnabled = true;
        if (player.TryGetPlayerPosition(out var position))
        {
            game.player.transform.position = position;
            ChangeGameState(GameStateID.Lobby);
        }
        else
        {
            StartCoroutine(EnterRoutine());
        }
    }

    private IEnumerator EnterRoutine()
    {
        yield return game.level.elevator.ShowAnimation(game.player);
        ChangeGameState(GameStateID.Lobby);
    }
}