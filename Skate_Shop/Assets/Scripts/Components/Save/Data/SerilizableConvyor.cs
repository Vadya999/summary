using System;

[Serializable]
public class SerilizableConvyor
{
    public SerializableStack boxStack;
    public SerializableStack skateStack;
    public int upgradeLevel;
    public int id;

    public SerilizableConvyor()
    {
        boxStack = new SerializableStack();
        skateStack = new SerializableStack();
        upgradeLevel = 0;
    }

    public SerilizableConvyor(SerializableStack boxStack, SerializableStack skateStack, int upgradeLevel, int id)
    {
        this.boxStack = boxStack;
        this.skateStack = skateStack;
        this.upgradeLevel = upgradeLevel;
        this.id = id;
    }
}
