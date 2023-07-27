using System.Collections;
using HomaGames.HomaBelly;
using Kuhpik;
using MoreMountains.NiceVibrations;
using Source.Scripts.Components;
using Source.Scripts.UI;
using UnityEngine;

namespace Source.Scripts.Systems
{
    public class InitGameSystem : GameSystemWithScreen<GameUIScreen>
    {
        public override void OnInit()
        {
            player.OnMoneyChanged += screen.UpdateMoneyText;
            screen.UpdateMoneyText(player.Money);
            
            game.playerComponent = FindObjectOfType<PlayerComponent>();
            game.tutorialArrowComponent = FindObjectOfType<TutorialArrowComponent>(true);
            game.showcaseComponents = FindObjectsOfType<ShowcaseComponent>();
            game.customerComponent = FindObjectOfType<CustomerComponent>();
            game.scannerComponent = FindObjectOfType<ScannerComponent>();
            game.scoreboardComponent = FindObjectOfType<ScoreboardComponent>();
            game.moneyZoneComponent = FindObjectOfType<MoneyZoneComponent>();
            game.customerPackageComponent = FindObjectOfType<CustomerPackageComponent>();
            game.cashBoxComponent = FindObjectOfType<CashBoxComponent>();
            game.cinemachineSwitchComponent = FindObjectOfType<CinemachineSwitchComponent>();

            StartCoroutine(GameLaunch());
        }

        private IEnumerator GameLaunch()
        {
            if (!player.firstLaunch)
            {
                player.firstLaunch = true;
                
                yield return new WaitForSeconds(1f);
                HomaBelly.Instance.TrackDesignEvent("first_game_launch");
                Bootstrap.Instance.SaveGame();
            }
        }
    }
}