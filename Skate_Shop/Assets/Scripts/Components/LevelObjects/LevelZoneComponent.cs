using UnityEngine;

public class LevelZoneComponent : MonoBehaviour
{
    [field: SerializeField] public GameObject showRoot { get; private set; }
    [field: SerializeField] public GameObject disableRoot { get; private set; }
    [field: SerializeField] public ParticleSystem unlockParticles { get; private set; }
    [field: SerializeField] public Transform unlockParticlesPoint { get; private set; }

    public void Show()
    {
        disableRoot.SetActive(false);
        showRoot.SetActive(true);
        Instantiate(unlockParticles, unlockParticlesPoint.position, Quaternion.identity);
        GetComponentInParent<LevelSegmentComponent>().navMesh.Build();
    }

    public void Hide()
    {
        showRoot.SetActive(false);
        disableRoot.SetActive(true);
    }
}
