using System;

[Serializable]
public class RoomProgress
{
    public int starCount = 0;

    public RoomProgress()
    {
    }

    public RoomProgress(int starCount)
    {
        this.starCount = starCount;
    }
}
