using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TowerSizeViewer : MonoBehaviour
{
    [SerializeField] private TMP_Text _sizeView;
    [SerializeField] private Tower _tower;

    private void OnEnable()
    {
        _tower.SizeUpdated += OnSizeUpdated;//подп
    }

    private void OnDisable()
    {
        _tower.SizeUpdated -= OnSizeUpdated;//отп
    }

    private void OnSizeUpdated(int size)//метод обр подписку
    {
        _sizeView.text = size.ToString();
        
    }
}
