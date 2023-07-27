using System;
using System.Linq;
using HomaGames.HomaBelly;
using Kuhpik;
using Snippets.Tutorial;

namespace Source.Scripts.Tutorial
{
    [Serializable]
    public class CustomerPackageStep : TutorialStep
        {
            public override void OnUpdate()
            {
            
            }

            protected override void OnBegin()
            {
                var package = Bootstrap.Instance.GameData.customerPackageComponent;
                package.OnMovedToPackage += Complete;
            
                Bootstrap.Instance.GameData.tutorialArrowComponent.SetTarget(package.transform);
            }

            protected override void OnComplete()
            {
                var package = Bootstrap.Instance.GameData.customerPackageComponent;
                package.OnMovedToPackage -= Complete;
                
                HomaBelly.Instance.TrackDesignEvent(eventKey);
            }
    }
}