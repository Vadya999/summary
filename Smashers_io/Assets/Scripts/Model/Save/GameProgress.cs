using System;

[Serializable]
public class GameProgress
{
    public LevelDictDictionary _levels = new LevelDictDictionary();

    public int GetStarCount(int level)
    {
        TryGenerate(level);
        return _levels[level].GetStarCount();
    }

    public void SetProgress(int level, int room, RoomProgress roomProgress)
    {
        TryGenerate(level);
        _levels[level].SetProgress(room, roomProgress);
    }

    public RoomProgress GetProgress(int level, int room)
    {
        TryGenerate(level);
        return _levels[level].GetProgress(room);
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
        _levels.Add(level, new LevelProgress());
    }

    private bool Exist(int level)
    {
        return _levels.Contains(level);
    }
}
