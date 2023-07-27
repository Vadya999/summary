using System;

[Serializable]
public class SettingsData
{
    public bool hapticEnabled;
    public bool soundEnabled;

    public SettingsData()
    {
        hapticEnabled = true;
        soundEnabled = true;
    }
}
