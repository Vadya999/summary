using UnityEngine;

public class RampAnimationComponent : MonoBehaviour
{
    [SerializeField] private Transform _skateRoot;

    public void SetSkate(SkateComponent skate)
    {
        var clone = Instantiate(skate, _skateRoot);
        clone.transform.localPosition = Vector3.zero;
        clone.transform.localRotation = Quaternion.identity;
        clone.transform.localScale = Vector3.one;
        clone.GetComponentInChildren<Outline>().UpdateMaterialProperties();
    }
}
