using System;

[Serializable]
public class SerializableItem
{
    public int id;
    public int skateLevel;

    public SerializableItem(int id)
    {
        this.id = id;
    }

    public SerializableItem(int id, int skateLevel)
    {
        this.id = id;
        this.skateLevel = skateLevel;
    }
}
