using UnityEngine;

public class PCInputDevice : IInputDevice
{
    public Vector3 movementDelta { get; private set; }
    public Vector3 movementOrigin { get; private set; }
    public bool clickedLastFraim { get; private set; }

    public bool isShowingVisual => false;

    private readonly string _horizontalAxisName = "Horizontal";
    private readonly string _verticalAxisName = "Vertical";

    public PCInputDevice()
    {
        movementOrigin = Vector3.one * 500;
    }

    public void Update()
    {
        clickedLastFraim = Input.GetMouseButtonDown(0);
        movementDelta = new Vector3(Input.GetAxisRaw(_horizontalAxisName), 0, Input.GetAxisRaw(_verticalAxisName));
    }
}
