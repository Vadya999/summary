using Cinemachine;
using Kuhpik;
using UnityEngine;
using UnityEngine.Events;
using UnityTools.Extentions;

public class UpgradeShopComponent : MonoBehaviour
{
    [field: SerializeField] public CinemachineVirtualCamera virtualCamera { get; private set; }

    public readonly UnityEvent<UpgradeShopComponent> Entered = new UnityEvent<UpgradeShopComponent>();

    private void Start()
    {
        virtualCamera.Follow = GameData.player.transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.HasComponent<PlayerComponent>() && enabled)
        {
            Entered?.Invoke(this);
        }
    }
}
