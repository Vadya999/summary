using System;
using System.Linq;
using HomaGames.HomaBelly;
using Kuhpik;
using Snippets.Tutorial;

namespace Source.Scripts.Tutorial
{
    [Serializable]
    public class ScannerStep : TutorialStep
        {
            public override void OnUpdate()
            {
            
            }

            protected override void OnBegin()
            {
                var scanner = Bootstrap.Instance.GameData.scannerComponent;
                scanner.OnScanning += Complete;
            
                Bootstrap.Instance.GameData.tutorialArrowComponent.SetTarget(scanner.transform);
            }

            protected override void OnComplete()
            {
                var scanner = Bootstrap.Instance.GameData.scannerComponent;
                scanner.OnScanning -= Complete;
                
                HomaBelly.Instance.TrackDesignEvent(eventKey);
            }
    }
}