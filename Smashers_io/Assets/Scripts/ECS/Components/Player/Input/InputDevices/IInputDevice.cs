using UnityEngine;

public interface IInputDevice
{
    public Vector3 movementDelta { get; }
    public Vector3 movementOrigin { get; }
    public bool isShowingVisual { get; }
    public bool clickedLastFraim { get; }

    public void Update();
}
