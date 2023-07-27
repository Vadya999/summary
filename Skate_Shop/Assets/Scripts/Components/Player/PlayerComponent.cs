using UnityEngine;

public class PlayerComponent : MonoBehaviour
{
    [field: SerializeField] public PlayerCameraComponent playersCamera { get; private set; }
    [field: SerializeField] public SkateStackComponent skatesRoot { get; private set; }
    [field: SerializeField] public BoxStackComponent boxRoot { get; private set; }
    [field: SerializeField] public PlayerWalletComponent walletComponent { get; private set; }
    [field: SerializeField] public TutorialPointer pointer { get; private set; }
}
