using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MiniPlayer : MonoBehaviour
{
    [SerializeField] private float moveTime;

    public bool OnPosition;

    public void MoveToPosition(Transform position)
    {
        var lookPos = position.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = rotation;
        transform.DOMove(position.position, moveTime).OnComplete(() => OnPosition = true);
    }
}