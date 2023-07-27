using Kuhpik;
using System.Collections.Generic;
using UnityEngine;

public class LoadSystem : GameSystem
{
    [SerializeField] private List<SerializationSegment> _saveSegments;

    public List<SerializationSegment> saveSegments => _saveSegments;

    private string _key = "settings";

    public override void OnGameStart()
    {
        game.shouldRestore = Load(out game.saveData);
        game.levelID = game.saveData.levelID;
        if (!game.shouldRestore) SDKEvents.progression.FirstGameLaunch();
    }

    public override void OnStateEnter()
    {
        ChangeGameState(GameStateID.LevelLoading);
    }

    public void Save()
    {
        var save = new SaveData();
        _saveSegments.ForEach(x => x.Save(save));
        save.levelID = game.levelID;
        Save<SaveData>(save);
    }

    private void Save<T>(SaveData settigns)
    {
        var jsonString = JsonUtility.ToJson(settigns);
        PlayerPrefs.SetString(_key, jsonString);
    }

    private bool Load<T>(out T value) where T : new()
    {
        if (PlayerPrefs.HasKey(_key))
        {
            var jsonString = PlayerPrefs.GetString(_key);
            value = JsonUtility.FromJson<T>(jsonString);
            return true;
        }
        else
        {
            value = new T();
            return false;
        }
    }
}
