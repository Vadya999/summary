using System;
using HomaGames.HomaBelly;
using Kuhpik;
using Snippets.Tutorial;

namespace Source.Scripts.Tutorial
{
    [Serializable]
    public class CashBoxStep : TutorialStep
    {
        public override void OnUpdate()
        {
        }

        protected override void OnBegin()
        {
            var cashBox = Bootstrap.Instance.GameData.cashBoxComponent;
            cashBox.OnMoneyPut += Complete;

            Bootstrap.Instance.GameData.tutorialArrowComponent.SetTarget(cashBox.transform);
        }

        protected override void OnComplete()
        {
            var cashBox = Bootstrap.Instance.GameData.cashBoxComponent;
            cashBox.OnMoneyPut -= Complete; 
            
            Bootstrap.Instance.GameData.tutorialArrowComponent.Deactivate();
            HomaBelly.Instance.TrackDesignEvent(eventKey);
            HomaBelly.Instance.TrackDesignEvent("tutorial_completed");
        }
    }
}