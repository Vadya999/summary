using System;
using System.Linq;
using HomaGames.HomaBelly;
using Kuhpik;
using Snippets.Tutorial;

namespace Source.Scripts.Tutorial
{
    [Serializable]
    public class ZoneMoneyStep : TutorialStep
    {
        public override void OnUpdate()
        {
        }

        protected override void OnBegin()
        {
            var moneyZone = Bootstrap.Instance.GameData.moneyZoneComponent;
            moneyZone.OnMoneyGet += Complete;

            Bootstrap.Instance.GameData.tutorialArrowComponent.SetTarget(moneyZone.transform);
        }

        protected override void OnComplete()
        {         
            var moneyZone = Bootstrap.Instance.GameData.moneyZoneComponent;
            moneyZone.OnMoneyGet -= Complete;
            
            HomaBelly.Instance.TrackDesignEvent(eventKey);
        }
    }
}