using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class Win : MonoBehaviour
    {
        [SerializeField] private Text nameText;
        [SerializeField] private TextMeshProUGUI winText;
        [SerializeField] private Button setButton;

        [SerializeField] private GameObject button;
        [SerializeField] private GameObject nameTextGO;
        [SerializeField] private GameObject winMenu;

        private string name;

        private void Start()
        {
            setButton.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            if (name!=null)
            {
                name = nameText.text.ToString();
            }
            
            button.SetActive(false);
            nameTextGO.SetActive(false);
        }

        public void Victory()
        {
            winMenu.SetActive(true);
            if (name == null)
            {
                const string winTextText = "YOU WIN";
                winText.text = winTextText;
            }
            else
            {
                const string win = "WIN";
                winText.text = name + win;
            }
        }
        
    }
}