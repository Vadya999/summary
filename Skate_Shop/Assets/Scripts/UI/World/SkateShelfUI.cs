using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkateShelfUI : MonoBehaviour
{
    [SerializeField] private SkateShelfComponent _shelf;

    [SerializeField] private Image _skateImage;
    [SerializeField] private TMP_Text _count;
    [SerializeField] private GameObject _root;

    private void Start()
    {
        OnRequiredSkateCountChanged(_shelf.skatersWaitingSkate);
    }

    private void OnEnable()
    {
        _shelf.RequiredSkateCountChanged += OnRequiredSkateCountChanged;
        _shelf.RequireSkateChanged += OnSkateChanged;
    }

    private void OnDisable()
    {
        _shelf.RequiredSkateCountChanged -= OnRequiredSkateCountChanged;
        _shelf.RequireSkateChanged -= OnSkateChanged;
    }

    private void OnSkateChanged(SkateData data, int level)
    {
        _skateImage.sprite = data.spriteSkateShelf[level];
    }

    private void OnRequiredSkateCountChanged(int count)
    {
        _count.text = count.ToString();
        _root.SetActive(count > 0);
    }
}
