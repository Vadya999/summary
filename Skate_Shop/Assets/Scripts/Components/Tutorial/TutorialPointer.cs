using UnityEngine;

public class TutorialPointer : MonoBehaviour
{
    [SerializeField] private GameObject _pointerRoot;

    private Transform _currentTarget;

    private float activeRange => TutorialConfig._arrowActiveRange;

    private void Awake()
    {
        SetTarget(null);
    }

    private void Update()
    {
        if (_currentTarget == null) return;
        UpdatePointerRotation();
        UpdateActive();
    }

    private void UpdatePointerRotation()
    {
        transform.rotation = Quaternion.LookRotation(_currentTarget.position - transform.position);
    }

    public void SetTarget(Transform target)
    {
        _currentTarget = target;
        _pointerRoot.SetActive(target != null);
    }

    private void UpdateActive()
    {
        _pointerRoot.SetActive(Vector3.Distance(transform.position, _currentTarget.position) > activeRange);
    }
}
