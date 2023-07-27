using Kuhpik;

public class SettingsSystem : GameSystemWithScreen<SettingScreen>
{
    public override void OnInit()
    {
        screen.SetState(player.soundEnabled);
    }

    public override void OnGameStart()
    {
        screen.SoundButtonClicked.AddListener(OnButtonClicked);
    }

    public override void OnGameEnd()
    {
        screen.SoundButtonClicked.RemoveListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        player.soundEnabled = !player.soundEnabled;
        screen.SetState(player.soundEnabled);
        Save();
    }
}
