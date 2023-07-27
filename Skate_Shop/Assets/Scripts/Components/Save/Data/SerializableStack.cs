using System;
using System.Collections.Generic;

[Serializable]
public class SerializableStack
{
    public List<SerializableItem> items;

    public SerializableStack()
    {
        items = new List<SerializableItem>();
    }

    public SerializableStack(List<SerializableItem> items)
    {
        this.items = items;
    }
}
