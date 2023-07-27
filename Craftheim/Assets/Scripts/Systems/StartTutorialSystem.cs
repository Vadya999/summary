using System.Collections;
using HomaGames.HomaBelly;
using Kuhpik;
using Snippets.Tutorial;
using UnityEngine;

namespace Source.Scripts.Systems
{
    public class StartTutorialSystem : GameSystem
    {
        public override void OnInit()
        {
            StartCoroutine(TutorialLaunch());
        }
        
        private IEnumerator TutorialLaunch()
        {
            yield return new WaitForSeconds(1f);
            FindObjectOfType<TutorialSystem>().Begin();
        }
    }
}