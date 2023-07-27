using System.Collections;
using UnityEngine;
using UnityTools.Extentions;

public class PressConveyor : MonoBehaviour
{
    [SerializeField] private Animator pressAnimator;
    [SerializeField] private ParticleSystem _pressEffect;
    [SerializeField] private Transform effectSpawnPos;

    private readonly int _isPrintID = Animator.StringToHash("is-print");

    private void OnTriggerEnter(Collider other)
    {
        if (other.HasComponent<SkateComponent>())
        {
            StartCoroutine(PressRoutine());
        }
    }

    private IEnumerator PressRoutine()
    {
        pressAnimator.SetTrigger(_isPrintID);
        yield return new WaitForSeconds(0.2f);
        Instantiate(_pressEffect, effectSpawnPos.position, Quaternion.Euler(100, 0, 0));
    }
}
