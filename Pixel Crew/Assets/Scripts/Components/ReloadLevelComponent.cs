using System.Collections;
using System.Collections.Generic;
using PixelCrew.Model;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PixelCrew.Components
{
    public class ReloadLevelComponent : MonoBehaviour
    {
        public void Reload()
        {
            var session = FindObjectOfType<GameSession>();
            DestroyImmediate(session);
            
            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name); 
        }
    }
}

