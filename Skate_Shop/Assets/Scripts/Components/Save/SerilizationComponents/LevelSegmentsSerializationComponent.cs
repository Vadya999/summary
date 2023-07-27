using System.Linq;
using UnityTools.Extentions;

public class LevelSegmentsSerializationComponent : SerializationSegment
{
    public override void Load(SaveData saveData)
    {
        bootstrap.GameData.segmentID = saveData.segmentID;
        bootstrap.GameData.level.segments.ForEach(x => x.gameObject.SetActive(false));
        bootstrap.GameData.level.segments.Take(saveData.segmentID + 1).ForEach(x => x.gameObject.SetActive(true));
    }

    public override void Save(SaveData saveData)
    {
        saveData.segmentID = bootstrap.GameData.segmentID;
    }
}
