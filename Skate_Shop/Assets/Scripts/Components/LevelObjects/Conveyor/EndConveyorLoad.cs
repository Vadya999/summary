using Kuhpik;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityTools.Extentions;

public class EndConveyorLoad : MonoBehaviour
{
    [SerializeField] private SkateStackComponent _skateStack;
    [SerializeField] private float delayTranslateSkates;

    private PlayerComponent player => GameData.player;

    private SkateStackComponent playerSkateStack => player.skatesRoot;
    private bool isStartTranslateSkatesToHendsPlayer;

    public SkateStackComponent skateStack => _skateStack;

    private void OnTriggerStay(Collider other)
    {
        if (other.HasComponent<PlayerComponent>() && !isStartTranslateSkatesToHendsPlayer)
        {
            StartCoroutine(SkatePickupRoutine());
        }
    }

    private IEnumerator SkatePickupRoutine()
    {
        isStartTranslateSkatesToHendsPlayer = true;
        yield return new WaitForSeconds(delayTranslateSkates);

        if (_skateStack.hasItem && !playerSkateStack.isFull)
        {
            var skate = _skateStack.TakeItem();
            playerSkateStack.AddToStack(skate);
        }
        yield return new WaitForSeconds(delayTranslateSkates);
        StopCoroutine(SkatePickupRoutine());
        isStartTranslateSkatesToHendsPlayer = false;
    }
}
