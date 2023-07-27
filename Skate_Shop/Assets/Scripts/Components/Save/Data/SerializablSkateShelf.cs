using System;

[Serializable]
public class SerializablSkateShelf
{
    public SerializableStack stack;
    public int id;

    public SerializablSkateShelf()
    {
        stack = new SerializableStack();
        id = 0;
    }

    public SerializablSkateShelf(SerializableStack stack, int id)
    {
        this.stack = stack;
        this.id = id;
    }
}
