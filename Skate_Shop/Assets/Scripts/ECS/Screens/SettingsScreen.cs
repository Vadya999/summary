using Kuhpik;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScreen : MonoBehaviour
{
    [SerializeField] private SettingsToggleUI _hapticButton;
    [SerializeField] private SettingsToggleUI _soundButton;
    [SerializeField] private GameObject _disableRoot;
    [SerializeField] private Button _settingsButton;

    private SettingsData _settings;

    private bool _settingsState;
    private bool settingsState
    {
        get => _settingsState;
        set
        {
            _settingsState = value;
            _disableRoot.SetActive(_settingsState);
        }
    }

    private void Start()
    {
        settingsState = false;
        _settings = GameData.settings;
        _hapticButton.SetState(_settings.hapticEnabled);
        _soundButton.SetState(_settings.soundEnabled);
    }
    

    private void OnEnable()
    {
        _hapticButton.onClick.AddListener(OnHapticButtonClicked);
        _soundButton.onClick.AddListener(OnSoundButtonClicked);
        _settingsButton.onClick.AddListener(SetSettingsState);
    }

    private void OnDisable()
    {
        _hapticButton.onClick.RemoveListener(OnHapticButtonClicked);
        _soundButton.onClick.RemoveListener(OnSoundButtonClicked);
        _settingsButton.onClick.RemoveListener(SetSettingsState);
    }

    private void OnHapticButtonClicked()
    {
        _settings.hapticEnabled = !_settings.hapticEnabled;
        _hapticButton.SetState(_settings.hapticEnabled);
    }

    private void OnSoundButtonClicked()
    {
        _settings.soundEnabled = !_settings.soundEnabled;
        _soundButton.SetState(_settings.soundEnabled);
    }

    private void SetSettingsState()
    {
        settingsState = !settingsState;
    }
}