using DG.Tweening;
using Kuhpik;
using MoreMountains.NiceVibrations;
using UnityEngine;
using UnityEngine.Events;
using UnityTools.Extentions;

public class MoneyComponent : MonoBehaviour
{
    [field: SerializeField] public int cost { get; set; }

    [SerializeField] private float translateSpeed;
    [SerializeField] private float lifeTime;
    [SerializeField] private Vector3 offsetTranslatePosition;

    private bool _isStartTranslate;

    public UnityEvent<MoneyComponent> Collected { get; private set; } = new UnityEvent<MoneyComponent>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.HasComponent<PlayerComponent>() && !_isStartTranslate)
        {
            TranslateMoneyToPlayer(GameData.player.transform);
        }
    }

    public void TranslateMoneyToPlayer(Transform playerTransform)
    {
        _isStartTranslate = true;
        GameData.walletModel.moneyCount += cost;

        transform.DOMove(playerTransform.position + offsetTranslatePosition, translateSpeed)
            .OnComplete(() =>
            {
                if (GameData.settings.hapticEnabled)
                {
                    MMVibrationManager.Haptic(HapticTypes.LightImpact);
                }
                Collected?.Invoke(this);
                transform.DOKill();
                StopAllCoroutines();
                Destroy(gameObject);
            });
    }
}
