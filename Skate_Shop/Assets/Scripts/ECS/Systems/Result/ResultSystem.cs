using Kuhpik;

public class ResultSystem : GameSystemWithScreen<NextLevelScreen>
{
    public override void OnStateEnter()
    {
        screen.nextLevelButton.onClick.AddListener(OnButtonClicked);
        Invoke(nameof(ShowResultScreen), 2);
    }

    public override void OnStateExit()
    {
        screen.nextLevelButton.onClick.RemoveListener(OnButtonClicked);
    }

    private void ShowResultScreen()
    {
        screen.Open();
    }

    private void OnButtonClicked()
    {
        screen.Close();
        game.levelID++;
        if (game.levelID > 2) game.levelID = 0;
        GameData.walletModel.moneyCount = 0;
        ChangeGameState(GameStateID.LevelLoading);
    }
}
