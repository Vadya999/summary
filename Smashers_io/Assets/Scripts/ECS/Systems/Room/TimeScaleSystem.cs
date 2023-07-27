using Kuhpik;
using UnityEngine;

public class TimeScaleSystem : GameSystem
{
    private WardrobeEnterSystem _wardrobeEnterSystem;
    private NeighborMovemnetSystem _neighborMovementSystem;

    public override void OnInit()
    {
        _wardrobeEnterSystem = GetSystem<WardrobeEnterSystem>();
        _neighborMovementSystem = GetSystem<NeighborMovemnetSystem>();
    }

    public override void OnUpdate()
    {
        if (_wardrobeEnterSystem.inWardrobe && !_neighborMovementSystem.isShowingTrapAnimation)
        {
            SetTimeScale(2);
        }
        else
        {
            SetTimeScale(1);
        }
    }

    private void SetTimeScale(float value)
    {
        Time.timeScale = value;
    }
}
