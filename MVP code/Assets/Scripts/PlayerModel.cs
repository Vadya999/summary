using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MVPScripts
{
    public class PlayerModel
    {
        private const string Expression = "expression";

        public void SaveGame(string expression)
        {
            string s = expression.Remove(expression.Length - 1);
            PlayerPrefs.SetString(Expression, s);
        }
    }
}

