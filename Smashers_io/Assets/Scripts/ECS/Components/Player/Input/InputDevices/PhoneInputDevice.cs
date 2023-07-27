using UnityEngine;

public class PhoneInputDevice : IInputDevice
{
    public Vector3 movementDelta { get; private set; }
    public Vector3 movementOrigin { get; private set; }
    public bool clickedLastFraim { get; private set; }

    public bool isShowingVisual => true;

    public void Update()
    {
        clickedLastFraim = Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
        if (Input.touchCount <= 0)
        {
            movementOrigin = Vector3.zero;
            movementDelta = Vector3.zero;
        }
        else
        {
            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                movementOrigin = touch.position;
            }
            else
            {
                var delta = touch.position - new Vector2(movementOrigin.x, movementOrigin.y);
                movementDelta = new Vector3(delta.x, 0, delta.y).normalized;
            }
        }
    }
}
