using System;

[Serializable]
public class SerializableUpgades
{
    public int speedLevel;
    public int capacityLevel;

    public SerializableUpgades()
    {
        speedLevel = 0;
        capacityLevel = 0;
    }

    public SerializableUpgades(int speedLevel, int capacityLevel)
    {
        this.speedLevel = speedLevel;
        this.capacityLevel = capacityLevel;
    }
}
