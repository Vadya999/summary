using System;

[Serializable]
public class LevelProgress
{
    public RoomDictionary _rooms = new RoomDictionary();

    public int GetStarCount()
    {
        var result = 0;
        foreach (var room in _rooms.Values)
        {
            result += room.starCount;
        }
        return result;
    }

    public void SetProgress(int room, RoomProgress roomProgress)
    {
        TryGenerate(room);
        _rooms[room] = roomProgress;
    }

    public RoomProgress GetProgress(int room)
    {
        TryGenerate(room);
        return _rooms[room];
    }

    private void TryGenerate(int level)
    {
        if (!Exist(level))
        {
            Generate(level);
        }
    }

    private void Generate(int level)
    {
        _rooms.Add(level, new RoomProgress());
    }

    private bool Exist(int level)
    {
        return _rooms.Contains(level);
    }
}
