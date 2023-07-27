using Kuhpik;
using UnityEngine.Events;

public class ElevatorSystem : GameSystemWithScreen<ElevatorScreen>
{
    private LevelLoadingSystem _levelLoadingSystem;
    private TargetPointerSystem _targetPointerSystem;

    private ElevatorComponent elevator => game.level.elevator;

    public readonly UnityEvent LevelStarted = new UnityEvent();
    public readonly UnityEvent LevelComplted = new UnityEvent();

    public override void OnInit()
    {
        _levelLoadingSystem = GetSystem<LevelLoadingSystem>();
        _targetPointerSystem = GetSystem<TargetPointerSystem>();
    }

    public override void OnStateEnter()
    {
        elevator.trigger.PlayerEntered.AddListener(OnPlayerEnterElevator);
        LevelStarted?.Invoke();
    }

    public override void OnStateExit()
    {
        elevator.trigger.PlayerEntered.RemoveListener(OnPlayerEnterElevator);
    }

    public override void OnUpdate()
    {
        var starCount = game.GetStarCount(player.currentLevelID);
        screen.SetStarCount(starCount);
        screen.SetLevelText(player.currentLevelID + 1);
        elevator.progress.text = $"{starCount}/{config.starRequiredToNextLevel[player.currentLevelID]}";
        elevator.SetState(_levelLoadingSystem.canChangeLevel);
        if (!_targetPointerSystem.hasTarget && _levelLoadingSystem.canChangeLevel)
        {
            _targetPointerSystem.SetTarget(elevator);
        }
    }

    private void OnPlayerEnterElevator(ElevatorComponent arg0)
    {
        LoadNextLevel();
    }

    private void LoadNextLevel()
    {
        if (_levelLoadingSystem.canChangeLevel)
        {
            LevelComplted?.Invoke();
            player.RemovePlyaerPostion();
            _levelLoadingSystem.LoadNextLevel();
        }
    }
}
