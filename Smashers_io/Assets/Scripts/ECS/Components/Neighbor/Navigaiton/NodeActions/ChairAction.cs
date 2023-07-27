using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityTools.Extentions;

namespace Navigation.Nodes.Actions
{
    [Serializable]
    public class ChairAction : NodeAction
    {
        [SerializeField] private float _sitTime;
        [SerializeField] private Transform _sitPoint;

        public override IEnumerator OnEnterPoint(NeighborComponent neighbor)
        {
            neighbor.animation.isManual = true;
            neighbor.idle.SetActive(true);
            yield return neighbor.transform.DoTransform(_sitPoint, 0.25f).WaitForCompletion();
            neighbor.animation.ShowSit();
            yield return new WaitForSeconds(_sitTime);
            neighbor.animation.ShowStandUp();
            yield return new WaitForSeconds(0.75f);
            neighbor.animation.isManual = false;
            neighbor.idle.SetActive(false);
        }
    }
}
