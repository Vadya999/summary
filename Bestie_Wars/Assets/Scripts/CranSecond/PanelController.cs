using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    [SerializeField] private Transform button;
    [SerializeField] private float speed;

    private Animator animator;

    private float currentY = 0.5f;
    private const float center = 0.5f;
    private const float buttonYClick = 0.005f;
    private const float buttonYStart = 0.02f;
    private bool isActivate;
    public float CurrentY => currentY;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Debug.Log(isActivate + " " + currentY);
        if (isActivate == false)
        {
            if (currentY > center)
            {
                currentY -= Time.deltaTime;
                if (currentY < center) currentY = center;
            }

            if (currentY < center)
            {
                currentY += Time.deltaTime;
                if (currentY > center) currentY = center;
            }
        }

        if (currentY > 1) currentY = 1;
        if (currentY < 0) currentY = 0;
        animator.SetFloat("Rotate", currentY);
    }

    public void Click()
    {
        button.DOKill();
        button.DOLocalMoveY(buttonYClick, 0.2f).OnComplete(() => button.DOLocalMoveY(buttonYStart, 0.2f));
    }
    public void Addy(float y)
    {
        currentY += y*speed*Time.deltaTime;
    }

    public void SetActivate(bool isActivate)
    {
        this.isActivate = isActivate;
    }
}