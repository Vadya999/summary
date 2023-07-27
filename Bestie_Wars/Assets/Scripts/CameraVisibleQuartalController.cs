using System.Collections;
using UnityEngine;

public class CameraVisibleQuartalController : MonoBehaviour
{
    [SerializeField] private Transform playerCar;

    private void Start()
    {
        StartCoroutine(RayCastCoroutine());
    }

    private IEnumerator RayCastCoroutine()
    {
        while (true)
        {
            RayCast();
            yield return new WaitForSeconds(0.3f);
        }
    }

    private void RayCast()
    {
        float maxRange = 50;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, (playerCar.position - transform.position), out hit, maxRange))
        {
            var quaratal = hit.transform.GetComponent<QuartalController>();
            if (quaratal)
            {
                quaratal.SetDisable();
            }
        }
    }
}