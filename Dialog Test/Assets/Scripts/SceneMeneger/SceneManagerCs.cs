using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerCs : MonoBehaviour
{
   [SerializeField] private string _sceneName;

   public void LoadingScene()
   {
      SceneManager.LoadScene(_sceneName);
   }
}
