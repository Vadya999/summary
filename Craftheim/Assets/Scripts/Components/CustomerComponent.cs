using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kuhpik;
using UnityEngine;

namespace Source.Scripts.Components
{
    public class CustomerComponent : MonoBehaviour
    {
        [SerializeField] private GameObject[] models;

        private GameObject currentModel;
        private int index;

        public void SetRandomModel()
        {
            if (index == models.Length) index = 0;
            
            for (var i = 0; i < models.Length; i++)
            {
                var model = models[i];
                model.SetActive(i == index);
                if (i == index) currentModel = model;
            }

            index++;
        }

        public async void PayMoney(Action callback = null)
        {
            currentModel.GetComponent<Animator>().SetTrigger("Pay");

            await Task.Delay(2000);
            callback?.Invoke();
        }
    }
}