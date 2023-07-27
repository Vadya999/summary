using System;
using System.Linq;
using HomaGames.HomaBelly;
using Kuhpik;
using Snippets.Tutorial;
using Source.Scripts.Components;
using UnityEngine;

namespace Source.Scripts.Tutorial
{
    [Serializable]
    public class ShowcaseStep : TutorialStep
    {
        private ShowcaseComponent showcase;
        
        public override void OnUpdate()
        {
            
        }

        protected override void OnBegin()
        {
            var id = Bootstrap.Instance.GameData.neededItemId;
            showcase = Bootstrap.Instance.GameData.showcaseComponents.First(x => x.CanGet(id));
            showcase.OnItemGet += Complete;
            
            Bootstrap.Instance.GameData.tutorialArrowComponent.SetTarget(showcase.transform);
        }

        protected override void OnComplete()
        {
            showcase.OnItemGet -= Complete;
            HomaBelly.Instance.TrackDesignEvent(eventKey);
        }
    }
}