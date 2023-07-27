using System;

[Serializable]
public class SaveData
{
    public SettingsData settings;

    public int levelID;
    public int segmentID;
    public SerializabeLevel level;
    public SerializableUpgades upgrades;

    public int moneyCount;
    public SerializableTransform playerTransform;

    public SerializableStack skateStack;
    public SerializableStack boxStack;

    public SaveData()
    {
        moneyCount = 0;
        settings = new SettingsData();
        upgrades = new SerializableUpgades();
        level = new SerializabeLevel();
        playerTransform = new SerializableTransform();
    }
}
