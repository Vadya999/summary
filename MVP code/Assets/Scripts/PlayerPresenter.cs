using System.Linq;
using System.Text;
using MVPScripts;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace MVPScripts
{
    public class PlayerPresenter
    {
        private PlayerModel _playerModel;
        private PlayerView _playerView;

        private string convertFromUIText;
        private char[] currentChars = new[] {'1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '/'};
        private char[] chars;
        private char fraction = '/';

        private bool isCurrentChars;
        private bool isExistfraction;
        private bool isCurrentPositionofFraction;

        private int firstValueIndexForFirst = -1;
        private int secondValueIndexForEnd = 0;
        private float firstValueFloat;
        private float secondValueFloat;
        private float result;
        
        private const string Expression = "expression";

        public PlayerPresenter(PlayerView view, PlayerModel model)
        {
            _playerModel = model;
            _playerView = view;

            Start();
        }

        private void Start()
        {
            _playerView.setExpressionAction += SolutionExpression;
            _playerView.saveExpression += SaveExpression;
            _playerView.newSolutionButton.onClick.AddListener(NewSolution);
            _playerView.exitButton.onClick.AddListener(Exit);
            
            GetSaveData();
        }

        private void SolutionExpression(TextMeshProUGUI text)
        {
            ClearChecks();
            
            CheckChars(text);
            if (isCurrentChars) CheckExistFraction(chars);
            if (isExistfraction) CheckCurrentPositionOfFraction(chars);

            if (isCurrentPositionofFraction) GetValuesIndex(chars);
            if (isCurrentPositionofFraction) ConvertValuesToFloat(firstValueIndexForFirst, secondValueIndexForEnd);
            if (isCurrentPositionofFraction) DivisionOfNumbers();
            if (isCurrentPositionofFraction) ViewResult();
        }

        private void GetSaveData()
        {
            _playerView.ViewSaveExpression(PlayerPrefs.GetString(Expression));
        }

        private void SaveExpression(TextMeshProUGUI expression)
        {
            string s = expression.text.ToString();
            _playerModel.SaveGame(s);
        }

        private void ViewResult()
        {
            _playerView.OutputResult(result);
        }

        private void DivisionOfNumbers()
        {
            result = firstValueFloat / secondValueFloat;
        }

        private void ConvertValuesToFloat(int firstValueIndexForFirst1, int secondValueIndexForEnd1)
        {
            StringBuilder s1 = new StringBuilder();
            StringBuilder s2 = new StringBuilder();

            for (int i = 0; i < firstValueIndexForFirst1; i++)
            {
                s1.Append(chars[i]);
            }

            for (int i = firstValueIndexForFirst1 + 1; i < chars.Length; i++)
            {
                s2.Append(chars[i]);
            }

            string stringValuefirst = s1.ToString();
            string stringValueSecond = s2.ToString();

            firstValueFloat = float.Parse(stringValuefirst);
            secondValueFloat = float.Parse(stringValueSecond);
        }

        private void GetValuesIndex(char[] chars)
        {
            int posFraction = 0;

            for (int i = 0; i < chars.Length; i++)
            {
                if (chars[i] == fraction)
                {
                    posFraction = i + 1;
                }
            }

            for (int i = 0; i < chars.Length; i++)
            {
                firstValueIndexForFirst++;

                if (i >= posFraction)
                {
                    firstValueIndexForFirst--;
                    secondValueIndexForEnd++;
                }
            }
        }

        private void CheckCurrentPositionOfFraction(char[] chars)
        {
            if (chars.Length > 2 && chars.First() != fraction && chars.Last() != fraction)
            {
                isCurrentPositionofFraction = true;
            }
        }

        private void CheckExistFraction(char[] charsString)
        {
            for (int i = 0; i < charsString.Length; i++)
            {
                if (charsString[i] == fraction)
                {
                    isExistfraction = true;
                    return;
                }
            }
        }

        private void NewSolution()
        {
            _playerView.ClearInputText();
            _playerView.NewSolution();
        }

        private void Exit()
        {
            _playerView.ClearInputText();
            Application.Quit();
        }

        private void CheckChars(TextMeshProUGUI value)
        {
            string s = value.text.Remove(value.text.Length - 1);
            int countChars = s.Length;
            int countCurrentChars = 0;
            chars = s.ToCharArray();

            for (int i = 0; i < chars.Length; i++)
            {
                char myChar = chars[i];

                for (int j = 0; j < currentChars.Length; j++)
                {
                    if (myChar == currentChars[j])
                    {
                        countCurrentChars++;
                    }
                }
            }

            if (countChars == countCurrentChars) isCurrentChars = true;
            else
            {
                _playerView.OpenErrorMenu(1);
            }
        }

        private void ClearPrefs()
        {
            PlayerPrefs.SetString(Expression, null);
        }

        private void ClearChecks()
        {
            ClearPrefs();

            isCurrentChars = false;
            isExistfraction = false;
            isCurrentPositionofFraction = false;

            firstValueIndexForFirst = -1;
            secondValueIndexForEnd = 0;
            
        }
    }
}