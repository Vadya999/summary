using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    [SerializeField] private Transform _rixationPoint;

    public Transform FixationPoint => _rixationPoint;//публичное свойство его можно получить но не изменять
}
