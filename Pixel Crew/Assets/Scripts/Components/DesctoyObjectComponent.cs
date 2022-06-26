using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace PixelCrew.Components
{
   public class DesctoyObjectComponent : MonoBehaviour
   {
      [SerializeField] private GameObject _objectToDestroy;
   
      public void DestroyObject()
      {
         Destroy(_objectToDestroy);
      }
   }
}

