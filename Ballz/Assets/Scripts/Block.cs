using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Block : MonoBehaviour
{
    private float hitsRemaining = 5;
    private SpriteRenderer spriteRenderer;
    private TextMeshPro text;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        text = GetComponentInChildren<TextMeshPro>();
        text.SetText(hitsRemaining.ToString());
    }

    private void UpdateVisualState()
    {
        text.SetText(hitsRemaining.ToString());
        spriteRenderer.color = Color.Lerp(Color.white, Color.red, hitsRemaining / 10);
    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        hitsRemaining--;
        if (hitsRemaining > 0 )
        {
            UpdateVisualState();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    internal void SetHits(int hits)
    {
        hitsRemaining = hits;
        UpdateVisualState();
    }
}
