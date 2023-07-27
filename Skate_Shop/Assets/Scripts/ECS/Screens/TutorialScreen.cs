using Kuhpik;
using TMPro;
using UnityEngine;

public class TutorialScreen : UIScreen
{
    [SerializeField] private TMP_Text _label;
    [SerializeField] private GameObject _root;

    private void Start()
    {
        SetTutorialText(string.Empty);
    }

    public void SetTutorialText(string text)
    {
        _root.SetActive(!string.IsNullOrEmpty(text));
        _label.text = text.ToUpper();
    }
}