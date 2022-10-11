using System;
using System.Globalization;
using Geniral_Settings;
using TMPro;
using TMPro.Examples;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace HUD
{
    public class SettingsSetupper : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private GeniralSettings _geniralSettings;
        [SerializeField] private Factory.Factory _factory;
        [SerializeField] private GameObject HUD;

        [Header("Text")]
        [SerializeField] private TextMeshProUGUI speedValue;
        [SerializeField] private TextMeshProUGUI travelledValue;
        [SerializeField] private TextMeshProUGUI spawnSpeedValue;

        [SerializeField] private Button startButton;

        private float valueFloat;

        private void Start()
        {
            startButton.onClick.AddListener(OnSaveInputText);
        }

        private void OnSaveInputText()
        {
            float speed = ParseTextToValue(speedValue);
            _geniralSettings.speedCube = speed;
            
            float travelledDistance = ParseTextToValue(travelledValue);
            _geniralSettings.traveledDistance = travelledDistance;
            
            float spawnValue = ParseTextToValue(spawnSpeedValue);
            _geniralSettings.spawnTime = spawnValue;

            _factory.isSettingsIntroducted = true;
            HUD.SetActive(false);
        }

        private void OnDestroy()
        {
            startButton.onClick.RemoveListener(OnSaveInputText);
        }

        private float ParseTextToValue(TextMeshProUGUI currentText)
        {

            if (currentText.text.Length == 0)
            {
                return 1f;
            }
            string valueString = currentText.text.Remove(currentText.text.Length-1);
            valueFloat = float.Parse(valueString);

             if (valueFloat > 0 && valueFloat < 100)
            {
                return valueFloat;
            }
            else
            {
                return 5f;
            }
        }
        
    }
}