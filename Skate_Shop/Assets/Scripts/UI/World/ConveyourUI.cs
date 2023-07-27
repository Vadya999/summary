using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConveyourUI : MonoBehaviour
{
    [SerializeField] private Image _skakteImage;
    [SerializeField] private TMP_Text _count;

    public void UpdateCapacityText(BoxStackComponent stack)
    {
        _count.text = $"{stack.count}/{stack.capacity}";
    }

    public void SetSkateImage(Sprite sprite)
    {
        _skakteImage.sprite = sprite;
    }
}
