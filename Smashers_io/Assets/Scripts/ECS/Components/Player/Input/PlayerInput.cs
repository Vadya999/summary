using UnityEngine;

public class PlayerInput
{
    public Vector3 movementDelta => isEnabled ? _inputDevice.movementDelta.normalized : Vector3.zero;
    public Vector3 movementOrigin => _inputDevice.movementOrigin;
    public bool isShowingVisual => _inputDevice.isShowingVisual;
    public void Update() => _inputDevice.Update();
    public bool clickedLastFraim => _inputDevice.clickedLastFraim;

    private IInputDevice _inputDevice;

    public static bool isEnabled = true;

    public PlayerInput()
    {
        _inputDevice = Application.platform switch
        {
            RuntimePlatform.WindowsEditor => new MouseInputDevice(),
            RuntimePlatform.OSXPlayer => new PCInputDevice(),
            RuntimePlatform.IPhonePlayer => new PhoneInputDevice(),
            RuntimePlatform.Android => new PhoneInputDevice(),
            _ => new PhoneInputDevice(),
        };
    }
}
