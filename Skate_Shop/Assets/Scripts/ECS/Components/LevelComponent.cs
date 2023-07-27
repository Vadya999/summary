using Kuhpik;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityTools.Extentions;

public class LevelComponent : MonoBehaviour
{
    public List<LevelSegmentComponent> segments { get; private set; }
    public PlayerComponent player { get; private set; }

    private void Awake()
    {
        segments = GetComponentsInChildren<LevelSegmentComponent>().ToList();
        player = GetComponentInChildren<PlayerComponent>();
    }

    private void Start()
    {
        Bootstrap.Instance.GameData.level.segments.ForEach(x => x.gameObject.SetActive(false));
        Bootstrap.Instance.GameData.level.segments.Take(Bootstrap.Instance.GameData.segmentID + 1).ForEach(x => x.gameObject.SetActive(true));
    }
}
