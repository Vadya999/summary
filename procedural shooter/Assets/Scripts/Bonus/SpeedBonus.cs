using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpeedBonus : MonoBehaviour
{
    public UnityEvent _event;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _event?.Invoke();
        }
    }
}
