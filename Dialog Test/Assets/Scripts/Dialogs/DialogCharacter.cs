using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogCharacter : MonoBehaviour
{
    [SerializeField] private List<string> _read = new List<string>();

    public SpriteRenderer _renderer;

    public int GetReadCount()
    {
        return _read.Count;
    }

    public string GetStringIndex(int i)
    {
        return _read[i];
    }
}
