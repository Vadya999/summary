using Kuhpik;
using UnityEngine;

public class SaveTriggerSystem : GameSystem
{
    [SerializeField] private LoadSystem _loadSystem;

    private void Save()
    {
        _loadSystem.Save();
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus && Application.platform != RuntimePlatform.WindowsEditor)
        {
            Save();
        }
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause && Application.platform != RuntimePlatform.WindowsEditor)
        {
            Save();
        }
    }

    public void OnEnable()
    {
        Application.quitting += Save;
    }

    public void OnDisable()
    {
        Application.quitting -= Save;
    }
}