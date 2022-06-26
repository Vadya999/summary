using System;
using System.Collections;
using System.Collections.Generic;
using PixelCrew.Creatures;
using UnityEngine;

namespace PixelCrew.Components
{
    public class CoinsNumComponent : MonoBehaviour
    {
        [SerializeField] private int _numCoin;
        [SerializeField] private Hero _hero;

        public void AddCoin()
        {
            _hero.AddCoins(_numCoin);
        }
    }
}

