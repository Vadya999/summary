using UnityEngine;

public class MouseInputDevice : IInputDevice
{
    public Vector3 movementDelta { get; private set; }
    public Vector3 movementOrigin { get; private set; }
    public bool isShowingVisual => true;
    public bool clickedLastFraim { get; private set; }

    public void Update()
    {
        clickedLastFraim = Input.GetMouseButtonDown(0);
        if (Input.GetMouseButtonDown(0)) { movementDelta = Vector3.zero; movementOrigin = Input.mousePosition; }
        if (Input.GetMouseButtonUp(0)) { movementDelta = Vector3.zero; movementOrigin = Vector3.zero; }
        if (Input.GetMouseButton(0))
        {
            var delta = Input.mousePosition - movementOrigin;
            movementDelta = new Vector3(delta.x, 0, delta.y).normalized;
        }
    }
}
