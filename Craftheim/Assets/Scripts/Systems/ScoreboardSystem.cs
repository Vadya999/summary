using System.Collections.Generic;
using System.Linq;
using Kuhpik;
using Source.Scripts.Components;
using Source.Scripts.Signals;

namespace Source.Scripts.Systems
{
    public class ScoreboardSystem : GameSystem
    {
        private string previousItem;
        
        public override void OnInit()
        {
            game.scoreboardComponent.MoneyText.gameObject.SetActive(false);
            game.scoreboardComponent.Icon.gameObject.SetActive(false);
            
            Supyrb.Signals.Get<LoadScoreboardSignal>().AddListener(LoadScoreboard);
            Supyrb.Signals.Get<ScannerSignal>().AddListener(ShowPrice);
        }

        private void LoadScoreboard()
        {
            game.scoreboardComponent.Icon.gameObject.SetActive(true);
            game.scoreboardComponent.MoneyText.gameObject.SetActive(false);

            var configs = previousItem != null ? game.items.Where(x => x.Id != previousItem).ToList() : game.items;
            var randConfig = configs.GetRandom();
            previousItem = randConfig.Id;

            game.neededItemId = randConfig.Id;
            game.scoreboardComponent.Icon.sprite = randConfig.Icon;
            //game.scoreboardComponent.Icon.color = randConfig.Color;
            game.scoreboardComponent.MoneyText.text = randConfig.Price > 1? $"{randConfig.Price}$" : $"{randConfig.Price * 100}¢";
        }

        private void ShowPrice()
        {
            game.scoreboardComponent.MoneyText.gameObject.SetActive(true);
        }
    }
}