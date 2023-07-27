using Kuhpik;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityTools.Extentions;
using UnityTools.UI;

public class RoomProgressScreen : UIScreen
{
    [SerializeField] private List<StarComponent> _stars;
    [SerializeField] private ProgressBar _progresssBar;

    public void SetStarCount(int count, float progress)
    {
        _progresssBar.SetProgress(progress);
        _stars.ForEach(x => x.SetState(false));
        _stars.Take(count).ForEach(x => x.SetState(true));
    }
}
