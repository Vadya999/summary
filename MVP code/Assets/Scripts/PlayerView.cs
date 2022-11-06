using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MVPScripts 
{
    public class PlayerView : MonoBehaviour
    {
        public UnityAction<TextMeshProUGUI> setExpressionAction;
        public UnityAction<TextMeshProUGUI> saveExpression;
        
        public Button exitButton;
        public Button newSolutionButton;

        [SerializeField] private GameObject errorMenu;

        private TextMeshProUGUI inputExpression;
        private TMP_InputField outText;

        private Button resultButton;
        private const string Expression = "expression";

        private void Awake()
        {
            inputExpression = GetComponentInChildren<TextMeshProUGUI>();
            outText = GetComponentInChildren<TMP_InputField>();

            resultButton = GetComponentInChildren<Button>();
            resultButton.onClick.AddListener(SetExpression);
        }
        
        private void Update()
        {
            if (Input.anyKeyDown && !(Input.GetMouseButtonDown(0)) || (Input.GetMouseButtonDown(1) || (Input.GetMouseButtonDown(2))))
            {
                saveExpression?.Invoke(inputExpression);
            }
        }

        public void OpenErrorMenu(int i)
        {
            if (i == 1) errorMenu.SetActive(true);
            else errorMenu.SetActive(false);
        }

        public void OutputResult(float result)
        {
            outText.text = result.ToString();
        }

        public void ViewSaveExpression(string expression)
        {
            outText.text = PlayerPrefs.GetString(Expression);
        }

        public void NewSolution()
        {
            OpenErrorMenu(0);
        }

        public void ClearInputText()
        {
            outText.text = null;
        }

        private void SetExpression()
        {
            setExpressionAction?.Invoke(inputExpression);
        }
    }
}