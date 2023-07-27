using Kuhpik;

public class RoomProgressSystem : GameSystem
{
    public override void OnStateEnter()
    {
        game.level.rooms.ForEach(x => x.progress.gameObject.SetActive(true));
    }

    public override void OnStateExit()
    {
        game.level.rooms.ForEach(x => x.progress.gameObject.SetActive(false));
    }

    public override void OnUpdate()
    {
        foreach (var room in game.level.rooms)
        {
            var level = player.currentLevelID;
            var index = game.level.rooms.IndexOf(room);
            var progress = player.progress.GetProgress(level, index);
            var starCount = progress.starCount;
            room.progress.SetProgress(starCount);
        }
    }
}
