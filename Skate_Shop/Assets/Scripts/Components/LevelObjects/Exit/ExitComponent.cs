using UnityEngine;

public class ExitComponent : MonoBehaviour
{
    [SerializeField] private DoorAnimationComponent _doorAnimation;

    public void ShowAnimaiton()
    {
        _doorAnimation.Open();
    }
}