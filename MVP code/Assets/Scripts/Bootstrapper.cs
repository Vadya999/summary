using System;
using Unity.Mathematics;
using UnityEngine;

namespace MVPScripts
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private GameObject HUD;
        
        private PlayerModel _playerModel;
        private PlayerPresenter _playerPresenter;

        private GameObject hud;
        private PlayerView view;

        private void Start()
        {
            StartGame();
        }

        private void StartGame()
        {
            _playerModel = new PlayerModel();

            InstantiateHud();

            TakeHud();

            _playerPresenter = new PlayerPresenter(view, _playerModel);
        }
        
        private void TakeHud() => 
            view = hud.GetComponent<PlayerView>();

        private void InstantiateHud() => 
            hud = Instantiate(HUD, Vector3.zero, quaternion.identity);
    }
}