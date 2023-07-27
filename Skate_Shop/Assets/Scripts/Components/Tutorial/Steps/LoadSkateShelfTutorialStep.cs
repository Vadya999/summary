using System;
using UnityEngine;

[Serializable]
public class LoadSkateShelfTutorialStep : TutorialStep
{
    [SerializeField] private SkateShelfComponent _shelfToLoad;

    public override void Update()
    {
        if (_shelfToLoad.skateListInSheif.Count > 0)
        {
            Complete();
        }
    }
}
