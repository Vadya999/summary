using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.Components
{
    public class ScoreboardComponent : MonoBehaviour
    {
        [field:SerializeField] public TextMeshProUGUI MoneyText { get; private set; }
        [field:SerializeField] public Image Icon { get; private set; }
    }
}